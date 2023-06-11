namespace Calculator.Interfaces
{
    public interface IParser
    {
        IExpression Parse(string text, IErrors errors);
    }
}
