using Calculator.Enums;
using Calculator.Interfaces;

namespace Calculator.Entities
{
    public class ExpressionComponent : IExpressionComponent
    {
        public ComponentType Type { get; }
        public float Value { get; }
        public OperatorType Operator { get; }

        public ExpressionComponent(float val)
        {
            Value = val;
            Type = ComponentType.Value;
        }

        public ExpressionComponent(OperatorType operatorType)
        {
            Operator = operatorType;
            Type = ComponentType.Operator;
        }
    }
}
