using NINA.Astrometry.Interfaces;
using NINA.Core.Utility;
using NINA.InstructionMath.ExpressionUtil;
using NINA.InstructionMath.Util;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Expression = org.mariuszgromada.math.mxparser.Expression;

namespace NINA.InstructionMath.ExpressionEditor {
    public class ExpressionEditorViewModel : INotifyPropertyChanged, ICountable {

        public ExpressionEditorViewModel() {
            _keywordDescriptions = GetDescriptions();
            Keywords = new ObservableCollection<string>(_keywordDescriptions.Keys);
        }

        public event EventHandler RequestCloseWindow;        

        public CommandEvent EvaluateExpressionCommand {get; set;} = new CommandEvent();
        public CommandEvent CheckExpressionSyntaxCommand { get; set; } = new CommandEvent();
        public CommandEvent SaveCommand { get; set; } = new CommandEvent();
        public CommandEvent CancelCommand { get; set; } = new CommandEvent();

        public string Expression { get => _expression;
            set { 
                _expression = value;
                OnPropertyChanged();
            }
        }

        public string Result {
            get => _result;
            set {
                _result = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Keywords { get; }

        public string SelectedKeyword {
            get => _selectedKeyword; 
            set {
                _selectedKeyword = value;
                if (_keywordDescriptions.TryGetValue(value, out var description)) 
                {
                    SelectedKeywordDescription = description;
                }
                OnPropertyChanged();
            }
        }

        public string SelectedKeywordDescription {
            get => _selectedKeywordDescription;
            set {
                _selectedKeywordDescription = value;
                OnPropertyChanged();
            }
        }

        public int Count { get; private set; } = 0;

        private IExpressionItem _item;
        private INighttimeCalculator _nightCalc;
        private ExpressionVariables _expressionVariables;

        private string _expression;
        private string _result;
        private string _selectedKeyword;
        private string _selectedKeywordDescription;

        private readonly Dictionary<string, string> _keywordDescriptions;

        public void Init(IExpressionItem expressionItem, INighttimeCalculator nighttimeCalculator) {
            _item = expressionItem;
            _nightCalc = nighttimeCalculator;
            _expressionVariables = new ExpressionVariables(_nightCalc, expressionItem, this);

            Expression = _item.GetExpression();

            Logger.Info($"Get expression={_item.GetExpression()}");

            EvaluateExpressionCommand.Clicked += EvaluateExpressionCommand_Clicked;
            CheckExpressionSyntaxCommand.Clicked += CheckExpressionSyntaxCommand_Clicked;
            SaveCommand.Clicked += SaveCommand_Clicked;
            CancelCommand.Clicked += CancelCommand_Clicked;
        }

        private void CancelCommand_Clicked(object sender, EventArgs e) {
            Expression = _item.GetExpression();
            RequestCloseWindow?.Invoke(this, EventArgs.Empty);
        }

        private void SaveCommand_Clicked(object sender, EventArgs e) {
            _item.SetExpression(Expression);
            Logger.Info($"Saving expression = {Expression}");
            RequestCloseWindow?.Invoke(this, EventArgs.Empty);
        }

        private void CheckExpressionSyntaxCommand_Clicked(object sender, EventArgs e) {
            var expression = GetExpression();
            if (expression.checkSyntax()) {
                MessageBox.Show("Valid!");
            }
            else {
                MessageBox.Show(expression.getErrorMessage(), "Expression is not valid");
            }
        }

        private void EvaluateExpressionCommand_Clicked(object sender, EventArgs e) {
            Count++;
            var expression = GetExpression();
            var result = expression.calculate();
            Logger.Info($"expression={expression.getExpressionString()} result={result}");
            Result = result.ToString();
        }

        private Expression GetExpression() {
            var mathExpression = new Expression(Expression);
            _expressionVariables.AddToExpression(mathExpression);
            return mathExpression;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Dictionary<string, string> GetDescriptions() {
            return new Dictionary<string, string>() {
                {"[count]", "The count of the number of times the instruction has been executed.\n\n" +
                "In the case of conditions this means the number of times the block has been executed\n" +
                "In the editor (what you are looking at) this is the number of times calculate has been clicked"},
                {"[time]", "The current time\nThis is expressed in milliseconds since 1970-01-01 (Unix time)" },
                {"[astro_dawn]", "The next astronomical dawn\nThis is expressed in milliseconds since 1970-01-01 (Unix time)" },
                {"[astro_dusk]", "The next astronomical dusk\nThis is expressed in milliseconds since 1970-01-01 (Unix time)" }
            };
        }
    }
}
