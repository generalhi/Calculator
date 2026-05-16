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
    [TestCase("2+2222222222222222222.22222222", ExpectedResult = "2+2222222222222222222.22222222")]
    [TestCase("2*(2+2)", ExpectedResult = "2*(2+2)")]
    [TestCase("2(2+2)", ExpectedResult = "2*(2+2)")]
    [TestCase("(2+2)2", ExpectedResult = "(2+2)*2")]
    [TestCase("2,1+2", ExpectedResult = "Error: '2,1' not parsed.")]
    [TestCase("2^2+2", ExpectedResult = "Error: '2^2' not parsed.")]
    [TestCase("2++2", ExpectedResult = "Error: Double operator '++'.")]
    [TestCase("-2+3", ExpectedResult = "Error: Expression starts with operator '-'.")]
    [TestCase("2+", ExpectedResult = "Error: Expression ends with operator '+'.")]
    [TestCase("(2+2", ExpectedResult = "Error: Unbalanced parentheses.")]
    [TestCase("2+2)", ExpectedResult = "Error: Unbalanced parentheses.")]
    [TestCase("()", ExpectedResult = "Error: Empty parentheses.")]
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
