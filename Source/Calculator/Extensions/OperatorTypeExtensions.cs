using Calculator.Enums;
using Calculator.Helpers;

namespace Calculator.Extensions
{
    public static class OperatorTypeExtensions
    {
        public static char ToChar(this OperatorType type)
        {
            switch (type)
            {
                case OperatorType.Add:
                    return OperatorsHelper.Add;
                case OperatorType.Sub:
                    return OperatorsHelper.Sub;
                case OperatorType.Mul:
                    return OperatorsHelper.Mul;
                case OperatorType.Div:
                    return OperatorsHelper.Div;
                case OperatorType.Begin:
                    return OperatorsHelper.Begin;
                case OperatorType.End:
                    return OperatorsHelper.End;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static OperatorType ToOperator(this char c)
        {
            switch (c)
            {
                case OperatorsHelper.Add:
                    return OperatorType.Add;
                case OperatorsHelper.Sub:
                    return OperatorType.Sub;
                case OperatorsHelper.Mul:
                    return OperatorType.Mul;
                case OperatorsHelper.Div:
                    return OperatorType.Div;
                case OperatorsHelper.Begin:
                    return OperatorType.Begin;
                case OperatorsHelper.End:
                    return OperatorType.End;
                default:
                    throw new ArgumentOutOfRangeException(nameof(c), c, null);
            }
        }
    }
}
