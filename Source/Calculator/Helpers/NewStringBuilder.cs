using System.Buffers;

namespace Calculator.Helpers
{
    public ref struct NewStringBuilder
    {
        private const int BufferStartSize = 16;

        private int _position;
        private Span<char> _buffer;
        private readonly int _capacity = 0;

        public int Length => _buffer.Length;
        public ref char this[int index] => ref _buffer[index];

        public NewStringBuilder()
        {
            _position = 0;
            _buffer = new char[BufferStartSize];
        }

        public NewStringBuilder(int capacity = 0)
        {
            _position = 0;
            _buffer = new char[BufferStartSize];

            _capacity = capacity;
        }

        public void Clear()
        {
            _position = 0;
        }

        public void Append(char c)
        {
            if (_position >= _buffer.Length - 1)
            {
                ResizeBuffer(1);
            }

            _buffer[_position++] = c;
        }

        public void Append(ReadOnlySpan<char> str)
        {
            ResizeBuffer(str.Length);

            str.CopyTo(_buffer[_position..]);
            _position += str.Length;
        }

        public void AppendLine(ReadOnlySpan<char> str)
        {
            Append(str);
            Append(Environment.NewLine);
        }

        public override string ToString() => new(_buffer[.._position]);

        private void ResizeBuffer(int addLength)
        {
            var newSize = _position + addLength;
            if (newSize <= _buffer.Length)
            {
                return;
            }

            newSize = _capacity > 0 ? newSize + _capacity : newSize * 2;

            var rented = ArrayPool<char>.Shared.Rent(newSize);
            _buffer.CopyTo(rented);
            _buffer = rented;
            ArrayPool<char>.Shared.Return(rented);
        }
    }
}
