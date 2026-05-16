using Calculator.Enums;
using Calculator.Extensions;
using Calculator.Helpers;
using NUnit.Framework;

namespace Tests.Extensions;

[TestFixture]
public class OperatorTypeExtensionsTests
{
    [TestCase(OperatorType.Add, ExpectedResult = OperatorsHelper.Add)]
    [TestCase(OperatorType.Sub, ExpectedResult = OperatorsHelper.Sub)]
    [TestCase(OperatorType.Mul, ExpectedResult = OperatorsHelper.Mul)]
    [TestCase(OperatorType.Div, ExpectedResult = OperatorsHelper.Div)]
    public char ToCharTests(OperatorType type)
    {
        return type.ToChar();
    }

    [TestCase(OperatorsHelper.Add, ExpectedResult = OperatorType.Add)]
    [TestCase(OperatorsHelper.Sub, ExpectedResult = OperatorType.Sub)]
    [TestCase(OperatorsHelper.Mul, ExpectedResult = OperatorType.Mul)]
    [TestCase(OperatorsHelper.Div, ExpectedResult = OperatorType.Div)]
    public OperatorType ToOperatorTests(char c)
    {
        return c.ToOperator();
    }
}
