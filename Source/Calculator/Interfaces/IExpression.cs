namespace Calculator.Interfaces
{
    public interface IExpression
    {
        IExpressionComponent this[int index] { get; }
        int Count { get; }
        void Add(IExpressionComponent item);
        void Normalize();
        void ToReversePolishNotation(IErrors errors);
    }
}
