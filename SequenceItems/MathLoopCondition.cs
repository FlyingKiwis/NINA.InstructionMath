using Newtonsoft.Json;
using NINA.Sequencer.Conditions;
using NINA.Sequencer.SequenceItem;
using System.ComponentModel.Composition;
using org.mariuszgromada.math.mxparser;
using NINA.Astrometry.Interfaces;
using NINA.InstructionMath.ExpressionUtil;
using NINA.InstructionMath.ExpressionEditor;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows;
using Expression = org.mariuszgromada.math.mxparser.Expression;
using NINA.Core.Utility;
using NINA.Profile.Interfaces;

namespace NINA.InstructionMath.SequenceItems {

    [ExportMetadata("Name", "Math Loop")]
    [ExportMetadata("Description", "This will loop based on the provided expression")]
    [ExportMetadata("Icon", "Math_SVG")]
    [ExportMetadata("Category", "Instruction Math")]
    [Export(typeof(ISequenceCondition))]
    [JsonObject(MemberSerialization.OptIn)]
    public class MathLoopCondition : SequenceCondition, IExpressionItem, ICountable {

        [ImportingConstructor]
        public MathLoopCondition(INighttimeCalculator nighttimeCalculator) {
            _nighttimeCalculator = nighttimeCalculator;
            _expressionVariables = new ExpressionVariables(_nighttimeCalculator, this, this);

            foreach (var op in Enum.GetValues(typeof(OperatorEnum)).Cast<OperatorEnum>()) {
                OperatorItemSource.Add(ToOperatorComboboxItem(op));
            }
        }

        private readonly INighttimeCalculator _nighttimeCalculator;
        private readonly ExpressionVariables _expressionVariables;

        private string _expression = "";
        private OperatorEnum _operator = OperatorEnum.Equal;
        private double _targetValue = 0;
        private double? _lastResult;

        public OpenExpressionEditor OpenEditorCommand { 
            get => new OpenExpressionEditor(this, _nighttimeCalculator);
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
                RaisePropertyChanged(nameof(OperatorText));
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

        public string TargetValueText {
            get => TargetValue.ToString();
            set {
                if(double.TryParse(value, out var targetValue)) {
                    TargetValue = targetValue;
                    RaisePropertyChanged();
                }
            }
        }

        public double? LastResult {
            get => _lastResult;
            set {
                _lastResult = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ResultVisibility));
            }
        }

        public Visibility ResultVisibility {
            get => LastResult.HasValue ? Visibility.Visible : Visibility.Hidden;
        }

        public string OperatorText {
            get => SelectedOperator.Value;
        }

        public ObservableCollection<KeyValuePair<OperatorEnum, string>> OperatorItemSource { get; set; } = new ObservableCollection<KeyValuePair<OperatorEnum, string>>();

        public KeyValuePair<OperatorEnum, string> SelectedOperator
        {
            get {
                return ToOperatorComboboxItem(Operator);
            }
            set {
                Operator = value.Key;
                RaisePropertyChanged();
            }
        }

        public int Count { get; private set; }

        public override void ResetProgress() {
            base.ResetProgress();

            Count = 0;
        }

        public override void SequenceBlockFinished() {
            base.SequenceBlockFinished();

            Count++;
        }


        public override bool Check(ISequenceItem previousItem, ISequenceItem nextItem) {
            var mathExpression = new Expression(Expression);
            _expressionVariables.AddToExpression(mathExpression);

            if (!mathExpression.checkSyntax()) {
                Logger.Error($"Expression syntax is invalid, expression: {mathExpression.getExpressionString()} error: {mathExpression.getErrorMessage()}");
                return false;
            }

            var result = mathExpression.calculate();
            LastResult = result;
            var continueLooping = evaluateResult(result);

            Logger.Info($"Expression: {Expression} Operator: {OperatorText} Target Value: {TargetValueText} Calculation: {result} Ending? {!continueLooping}");

            return continueLooping;
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
                Count = 0
            };
        }

        public string GetExpression() {
            Logger.Info($"Expression: {Expression}");
            return Expression;
        }

        public void SetExpression(string expression) {
            Logger.Info($"Expression: {expression}");
            Expression = expression;
        }

        /// <summary>
        /// This string will be used for logging
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"Category: {Category}, Item: {nameof(MathLoopCondition)} Expression: {Expression} Operator={OperatorText} TargetValue={TargetValueText}";
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
                case OperatorEnum.NotEqual:
                    return expressionResult != _targetValue;
                default:
                    return false;
            }
        }

        private KeyValuePair<OperatorEnum, string> ToOperatorComboboxItem(OperatorEnum selectedOperator) {
            switch (selectedOperator) 
            {
                case OperatorEnum.GreaterThan:
                    return new KeyValuePair<OperatorEnum, string>(OperatorEnum.GreaterThan, ">");
                case OperatorEnum.LessThan:
                    return new KeyValuePair<OperatorEnum, string>(OperatorEnum.LessThan, "<");
                case OperatorEnum.GreaterThanOrEqual:
                    return new KeyValuePair<OperatorEnum, string>(OperatorEnum.GreaterThanOrEqual, ">=");
                case OperatorEnum.LessThanOrEqual:
                    return new KeyValuePair<OperatorEnum, string>(OperatorEnum.LessThanOrEqual, "<=");
                case OperatorEnum.Equal:
                    return new KeyValuePair<OperatorEnum, string>(OperatorEnum.Equal, "=");
                case OperatorEnum.NotEqual:
                    return new KeyValuePair<OperatorEnum, string>(OperatorEnum.NotEqual, "!=");
                default:
                    throw new System.ArgumentException($"{selectedOperator} is unknown", nameof(selectedOperator));

            }
        }
    }
}