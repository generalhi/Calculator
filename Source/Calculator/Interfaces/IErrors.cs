namespace Calculator.Interfaces
{
    public interface IErrors
    {
        bool IsPresent { get; }
        void Clear();
        void Add(string error);
        void Print();
    }
}
