using Calculator.Enums;
using Calculator.Interfaces;

namespace Calculator.Entities
{
    public class ExpressionComponent : IExpressionComponent
    {
        private readonly float _value;
        private readonly OperatorType _operator;

        public ComponentType Type { get; private init; }

        public float Value
        {
            get => _value;
            private init
            {
                _value = value;
                Type = ComponentType.Value;
            }
        }

        public OperatorType Operator
        {
            get => _operator;
            private init
            {
                _operator = value;
                Type = ComponentType.Operator;
            }
        }

        public ExpressionComponent(float val)
        {
            Value = val;
        }

        public ExpressionComponent(OperatorType operatorType)
        {
            Operator = operatorType;
        }
    }
}
