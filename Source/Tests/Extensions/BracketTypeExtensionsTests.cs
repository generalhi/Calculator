using Calculator.Enums;
using Calculator.Extensions;
using Calculator.Helpers;
using NUnit.Framework;

namespace Tests.Extensions;

[TestFixture]
public class BracketTypeExtensionsTests
{
    [TestCase(BracketType.Open, ExpectedResult = BracketsHelper.Open)]
    [TestCase(BracketType.Close, ExpectedResult = BracketsHelper.Close)]
    public char ToCharTests(BracketType type)
    {
        return type.ToChar();
    }

    [TestCase(BracketsHelper.Open, ExpectedResult = BracketType.Open)]
    [TestCase(BracketsHelper.Close, ExpectedResult = BracketType.Close)]
    public BracketType ToBracketTests(char c)
    {
        return c.ToBracket();
    }
}
