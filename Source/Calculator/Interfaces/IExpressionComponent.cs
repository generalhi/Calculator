using Calculator.Enums;

namespace Calculator.Interfaces
{
    public interface IExpressionComponent
    {
        ComponentType Type { get; }
        decimal Value { get; }
        OperatorType Operator { get; }
        BracketType Bracket { get; }
    }
}
