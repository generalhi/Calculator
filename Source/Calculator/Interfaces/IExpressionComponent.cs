using Calculator.Enums;

namespace Calculator.Interfaces
{
    public interface IExpressionComponent
    {
        ComponentType Type { get; }
        float Value { get; }
        OperatorType Operator { get; }
    }
}
