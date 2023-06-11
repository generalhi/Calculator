using Calculator.Interfaces;

namespace Calculator
{
    public class Errors : IErrors
    {
        private readonly List<string> _items = new List<string>();

        public bool IsPresent => _items.Count > 0;

        public string this[int index] => _items[index];
        
        public void Clear()
        {
            _items.Clear();
        }

        public void Add(string error)
        {
            _items.Add(error);
        }

        public void Print()
        {
            if (_items.Count == 0)
            {
                return;
            }

            Console.WriteLine("Errors:");
            foreach (var item in _items)
            {
                Console.WriteLine($"- {item}");
            }
        }
    }
}
