using Newtonsoft.Json;
using NINA.Sequencer.Conditions;
using NINA.Sequencer.SequenceItem;
using System.ComponentModel.Composition;
using org.mariuszgromada.math.mxparser;
using NINA.Astrometry.Interfaces;
using NINA.InstructionMath.ExpressionUtil;
using NINA.InstructionMath.ExpressionEditor;
using NINA.Core.Utility;

namespace NINA.InstructionMath.SequenceItems {

    [ExportMetadata("Name", "Math Loop")]
    [ExportMetadata("Description", "This will loop based on the provided expression")]
    [ExportMetadata("Icon", "Math_SVG")]
    [ExportMetadata("Category", "Instruction Math")]
    [Export(typeof(ISequenceCondition))]
    [JsonObject(MemberSerialization.OptIn)]
    public class MathLoopCondition : SequenceCondition, IExpressionItem {
        
        [ImportingConstructor]
        public MathLoopCondition(INighttimeCalculator nighttimeCalculator) {
            _nighttimeCalculator = nighttimeCalculator;
            _expressionVariables = new ExpressionVariables(_nighttimeCalculator);

            Expression = "Test";
            TargetValue = 0;
            Operator = OperatorEnum.Equal;

            OpenEditorCommand = new OpenExpressionEditor(this, _expressionVariables);
        }

        private readonly INighttimeCalculator _nighttimeCalculator;
        private readonly ExpressionVariables _expressionVariables;

        private string _expression;
        private OperatorEnum _operator;
        private double _targetValue;
        private int _loopCount = 0;
        private OpenExpressionEditor _openEditorCommand;

        public OpenExpressionEditor OpenEditorCommand { get => _openEditorCommand;
                set { 
                    _openEditorCommand = value;
                    RaisePropertyChanged();
            } 
        }

        [JsonProperty]
        public string Expression {
            get => _expression;
            set {
                _expression = value;
                RaisePropertyChanged();
            }
        }

        [JsonProperty]
        public OperatorEnum Operator {
            get => _operator;
            set {
                _operator = value;
                RaisePropertyChanged();
            }
        }

        [JsonProperty]
        public double TargetValue {
            get => _targetValue;
            set {
                _targetValue = value;
                RaisePropertyChanged();
            }
        }

        public override bool Check(ISequenceItem previousItem, ISequenceItem nextItem) {
            _loopCount++;
            var mathExpression = new Expression(_expression);
            addExpressionVariables(mathExpression);
            var result = mathExpression.calculate();

            return evaluateResult(result);
        }

        public override object Clone() {
            return new MathLoopCondition(_nighttimeCalculator) {
                Icon = Icon,
                Name = Name,
                Category = Category,
                Description = Description,
                Expression = Expression,
                TargetValue = TargetValue,
                Operator = Operator,
            };
        }

        /// <summary>
        /// This string will be used for logging
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"Category: {Category}, Item: {nameof(MathLoopCondition)}";
        }

        private bool evaluateResult(double expressionResult) {

            switch (_operator) {
                case OperatorEnum.GreaterThan:
                    return expressionResult > _targetValue;
                case OperatorEnum.LessThan:
                    return expressionResult < _targetValue;
                case OperatorEnum.GreaterThanOrEqual:
                    return expressionResult >= _targetValue;
                case OperatorEnum.LessThanOrEqual:
                    return expressionResult <= _targetValue;
                case OperatorEnum.Equal:
                    return expressionResult == _targetValue;
                default:
                    return false;
            }
        }

        private void addExpressionVariables(Expression expression) 
        {
            var loopCount = new Constant("$count", _loopCount);
            _expressionVariables.AddToExpression(expression);
        }
    }
}