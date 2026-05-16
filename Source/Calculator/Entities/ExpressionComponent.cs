using Calculator.Enums;
using Calculator.Interfaces;

namespace Calculator.Entities
{
    public class ExpressionComponent : IExpressionComponent
    {
        public ComponentType Type { get; }
        public decimal Value { get; }
        public OperatorType Operator { get; }
        public BracketType Bracket { get; }

        public ExpressionComponent(decimal val)
        {
            Value = val;
            Type = ComponentType.Value;
        }

        public ExpressionComponent(OperatorType operatorType)
        {
            Operator = operatorType;
            Type = ComponentType.Operator;
        }

        public ExpressionComponent(BracketType bracketType)
        {
            Bracket = bracketType;
            Type = ComponentType.Bracket;
        }
    }
}
