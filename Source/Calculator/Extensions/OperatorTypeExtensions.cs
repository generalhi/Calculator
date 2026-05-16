using Calculator.Enums;
using Calculator.Helpers;

namespace Calculator.Extensions
{
    public static class OperatorTypeExtensions
    {
        public static char ToChar(this OperatorType type) => type switch
        {
            OperatorType.Add => OperatorsHelper.Add,
            OperatorType.Sub => OperatorsHelper.Sub,
            OperatorType.Mul => OperatorsHelper.Mul,
            OperatorType.Div => OperatorsHelper.Div,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public static OperatorType ToOperator(this char c) => c switch
        {
            OperatorsHelper.Add => OperatorType.Add,
            OperatorsHelper.Sub => OperatorType.Sub,
            OperatorsHelper.Mul => OperatorType.Mul,
            OperatorsHelper.Div => OperatorType.Div,
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };
    }
}
