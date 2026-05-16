using Calculator.Entities;
using Calculator.Enums;
using NUnit.Framework;

namespace Tests.Entities;

[TestFixture]
public class ExpressionComponentTests
{
    [Test]
    public void InitTest1()
    {
        var c = new ExpressionComponent(1m);
        Assert.That(c.Type, Is.EqualTo(ComponentType.Value));
        Assert.That(c.Value, Is.EqualTo(1m));
    }

    [Test]
    public void InitTest2()
    {
        var c = new ExpressionComponent(OperatorType.Add);
        Assert.That(c.Type, Is.EqualTo(ComponentType.Operator));
        Assert.That(c.Operator, Is.EqualTo(OperatorType.Add));
    }

    [Test]
    public void InitTest3()
    {
        var c = new ExpressionComponent(BracketType.Open);
        Assert.That(c.Type, Is.EqualTo(ComponentType.Bracket));
        Assert.That(c.Bracket, Is.EqualTo(BracketType.Open));
    }
}
