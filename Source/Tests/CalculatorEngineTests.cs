using Calculator;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class CalculatorEngineTests
{
    [TestCase("2+2", ExpectedResult = 4)]
    [TestCase("2+2*2", ExpectedResult = 6)]
    [TestCase("2+2/2", ExpectedResult = 3)]
    [TestCase("3+4*2/(1-5)+2", ExpectedResult = 3)]
    [TestCase("(2+2)2+(2+2)/2", ExpectedResult = 10)]
    [TestCase("(2+(2+(2(2+(2+2)))))2+(2+2)/2", ExpectedResult = 34)]
    [TestCase("2(2+2)", ExpectedResult = 8)]
    [TestCase("(2+2)2", ExpectedResult = 8)]
    [TestCase("0.1+0.2", ExpectedResult = 0.3)]
    public decimal CalcSuccess(string text)
    {
        var calc = new CalculatorEngine();
        var result = calc.Run(text, new Parser(), new Errors());
        Assert.That(result, Is.Not.Null);
        return result!.Value;
    }

    [TestCase("1/0", ExpectedResult = "Error: Division by zero.")]
    [TestCase("2/(2-2)", ExpectedResult = "Error: Division by zero.")]
    [TestCase("-2+3", ExpectedResult = "Error: Expression starts with operator '-'.")]
    [TestCase("(2+2", ExpectedResult = "Error: Unbalanced parentheses.")]
    [TestCase("2+2)", ExpectedResult = "Error: Unbalanced parentheses.")]
    [TestCase("()", ExpectedResult = "Error: Empty parentheses.")]
    [TestCase("2+", ExpectedResult = "Error: Expression ends with operator '+'.")]
    [TestCase("", ExpectedResult = "Math expression text not found!")]
    public string CalcError(string text)
    {
        var calc = new CalculatorEngine();
        var errors = new Errors();
        var result = calc.Run(text, new Parser(), errors);
        Assert.That(result, Is.Null);
        Assert.That(errors.IsPresent, Is.True);
        return errors[0];
    }
}
