using Calculator.Enums;
using Calculator.Helpers;

namespace Calculator.Extensions
{
    public static class BracketTypeExtensions
    {
        public static char ToChar(this BracketType type) => type switch
        {
            BracketType.Open => BracketsHelper.Open,
            BracketType.Close => BracketsHelper.Close,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public static BracketType ToBracket(this char c) => c switch
        {
            BracketsHelper.Open => BracketType.Open,
            BracketsHelper.Close => BracketType.Close,
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };
    }
}
