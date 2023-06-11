using Calculator;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class ParserTests
{
    [TestCase("2+2", ExpectedResult = "2+2")]
    [TestCase("2 +2", ExpectedResult = "2+2")]
    [TestCase("2+ 2", ExpectedResult = "2+2")]
    [TestCase("2.1+2", ExpectedResult = "2.1+2")]
    [TestCase("2+2222222222222222222.22222222", ExpectedResult = "2+2.2222223E+18")]
    [TestCase("2*(2+2)", ExpectedResult = "2*(2+2)")]
    [TestCase("2(2+2)", ExpectedResult = "2*(2+2)")]
    [TestCase("(2+2)2", ExpectedResult = "(2+2)*2")]
    [TestCase("2,1+2", ExpectedResult = "Error: '2,1' not parsed.")]
    [TestCase("2^2+2", ExpectedResult = "Error: '2^2' not parsed.")]
    [TestCase("2++2", ExpectedResult = "Error: Double operator '++'.")]
    [TestCase("", ExpectedResult = "Math expression text not found!")]
    [TestCase(null, ExpectedResult = "Math expression text not found!")]
    public string ExpressionTests(string text)
    {
        var e = new Errors();
        var p = new Parser();
        var expression = p.Parse(text, e);
        if (e.IsPresent)
        {
            return e[0];
        }
        return expression.ToString()!;
    }
}
