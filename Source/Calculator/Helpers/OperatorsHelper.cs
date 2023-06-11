using Calculator.Enums;

namespace Calculator.Helpers
{
    public static class OperatorsHelper
    {
        public const char Add = '+';
        public const char Sub = '-';
        public const char Mul = '*';
        public const char Div = '/';
        public const char Begin = '(';
        public const char End = ')';

        public static readonly HashSet<char> Possible =
            new HashSet<char> {Add, Sub, Mul, Div, Begin, End};

        public static readonly Dictionary<OperatorType, int> Priority =
            new Dictionary<OperatorType, int>()
            {
                {OperatorType.Begin, 0},
                {OperatorType.Add, 1},
                {OperatorType.Sub, 1},
                {OperatorType.Mul, 2},
                {OperatorType.Div, 2}
            };

        public static readonly Dictionary<OperatorType, Func<float, float, float>> Function =
            new Dictionary<OperatorType, Func<float, float, float>>
            {
                {OperatorType.Add, (a, b) => a + b},
                {OperatorType.Sub, (a, b) => a - b},
                {OperatorType.Mul, (a, b) => a * b},
                {OperatorType.Div, (a, b) => a / b}
            };
    }
}
