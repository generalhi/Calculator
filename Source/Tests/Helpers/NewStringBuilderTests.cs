using Calculator.Helpers;
using NUnit.Framework;

namespace Tests.Helpers;

[TestFixture]
public class NewStringBuilderTests
{
    [TestCase("0000000000000000", ExpectedResult = 16)]
    [TestCase("00000000000000000", ExpectedResult = 64)]
    [TestCase("000000000000000000", ExpectedResult = 64)]
    public int ResizeBufLengthTest(string text)
    {
        var sb = new NewStringBuilder();
        sb.Append(text);
        return sb.Length;
    }

    [Test]
    public void ClearTest()
    {
        var sb = new NewStringBuilder();
        sb.Append("begin");
        sb.Clear();
        sb.Append("end");

        Assert.That(sb.ToString(), Is.EqualTo("end"));
    }

    [Test]
    public void AppendCharTest()
    {
        var sb = new NewStringBuilder();
        sb.Append("0000000000000000");
        sb.Append('c');

        Assert.That(sb.ToString(), Is.EqualTo("0000000000000000c"));
        Assert.That(sb.Length, Is.EqualTo(64));
    }

    [Test]
    public void AppendTest()
    {
        var sb = new NewStringBuilder();
        sb.Append("begin");
        sb.Append(" ");
        sb.Append("end");

        Assert.That(sb.ToString(), Is.EqualTo("begin end"));
    }

    [Test]
    public void AppendLineTest()
    {
        var sb = new NewStringBuilder();
        sb.AppendLine("begin");
        sb.AppendLine("end");

        Assert.That(sb.ToString(), Is.EqualTo("begin\r\nend\r\n"));
    }
}
