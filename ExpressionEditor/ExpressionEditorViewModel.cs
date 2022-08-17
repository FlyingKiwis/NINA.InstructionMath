using NINA.Core.Utility;
using NINA.InstructionMath.ExpressionUtil;
using NINA.InstructionMath.Util;
using org.mariuszgromada.math.mxparser;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Expression = org.mariuszgromada.math.mxparser.Expression;

namespace NINA.InstructionMath.ExpressionEditor {
    public class ExpressionEditorViewModel : INotifyPropertyChanged {

        public ExpressionEditorViewModel() {
        }

        public event EventHandler RequestCloseWindow;        

        public CommandEvent EvaluateExpressionCommand {get; set;} = new CommandEvent();
        public CommandEvent CheckExpressionSyntaxCommand { get; set; } = new CommandEvent();
        public CommandEvent SaveCommand { get; set; } = new CommandEvent();
        public CommandEvent CancelCommand { get; set; } = new CommandEvent();

        public string Expression { get => _expression;
            set { 
                _expression = value;
                _mathExpression.setExpressionString(value);
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

        private IExpressionItem _item;
        private ExpressionVariables _expressionVariables;

        private string _expression;
        private string _result;
        private Expression _mathExpression;

        public void Init(IExpressionItem expressionItem, ExpressionVariables expressionVariables) {
            _item = expressionItem;
            _mathExpression = new Expression("");
            _expressionVariables = expressionVariables;

            Expression = _item.GetExpression();

            Logger.Info($"Get expression={_item.GetExpression()}");

            _mathExpression = new Expression(Expression);
            _expressionVariables.AddToExpression(_mathExpression);
            var count = new Constant("count", 0);
            _mathExpression.addConstants(count);

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
            if(_mathExpression.checkSyntax()) {
                MessageBox.Show("Valid!");
            }
            else {
                MessageBox.Show(_mathExpression.getErrorMessage(), "Expression is not valid");
            }
        }

        private void EvaluateExpressionCommand_Clicked(object sender, EventArgs e) {
            Logger.Info(_mathExpression.getExpressionString());
            var result = _mathExpression.calculate();
            Logger.Info($"result={result}");
            Result = result.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
