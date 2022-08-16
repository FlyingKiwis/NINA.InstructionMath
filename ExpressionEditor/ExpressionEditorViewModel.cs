using NINA.InstructionMath.ExpressionUtil;
using NINA.InstructionMath.Util;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NINA.InstructionMath.ExpressionEditor {
    public class ExpressionEditorViewModel : INotifyPropertyChanged {

        public ExpressionEditorViewModel() {
        }

        private IExpressionItem _item;
        private ExpressionVariables _expressionVariables;

        private string _expression;
        private string _result;
        private Expression _mathExpression;

        public CommandEvent EvaluateExpressionCommand {get; set;} = new CommandEvent();

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

        public void Init(IExpressionItem expressionItem, ExpressionVariables expressionVariables) {
            _item = expressionItem;
            _expressionVariables = expressionVariables;

            Expression = _item.Expression;
            _mathExpression = new Expression(Expression);
            _expressionVariables.AddToExpression(_mathExpression);
            var count = new Constant("$count", 0);
            _mathExpression.addConstants(count);

            EvaluateExpressionCommand.Clicked += EvaluateExpressionCommand_Clicked;
        }

        private void EvaluateExpressionCommand_Clicked(object sender, EventArgs e) {
            Result = _mathExpression.calculate().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
