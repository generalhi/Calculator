using System.Buffers;

namespace Calculator.Helpers
{
    public ref struct NewStringBuilder
    {
        private const int DefaultCapacity = 16;

        private int _position;
        private Span<char> _buffer;
        private char[]? _rentedBuffer;

        public int Capacity => _buffer.Length;
        public int Length => _position;
        public ref char this[int index] => ref _buffer[index];

        public NewStringBuilder()
        {
            _position = 0;
            _buffer = new char[DefaultCapacity];
            _rentedBuffer = null;
        }

        public NewStringBuilder(int initialCapacity)
        {
            if (initialCapacity <= 0)
            {
                initialCapacity = DefaultCapacity;
            }

            _position = 0;
            _buffer = new char[initialCapacity];
            _rentedBuffer = null;
        }

        public void Clear()
        {
            _position = 0;
        }

        public void Append(char c)
        {
            ResizeBuffer(1);
            _buffer[_position++] = c;
        }

        public void Append(scoped ReadOnlySpan<char> str)
        {
            ResizeBuffer(str.Length);

            str.CopyTo(_buffer[_position..]);
            _position += str.Length;
        }

        public void AppendLine(scoped ReadOnlySpan<char> str)
        {
            Append(str);
            Append(Environment.NewLine);
        }

        public override string ToString() => new(_buffer[.._position]);

        public void Dispose()
        {
            var toReturn = _rentedBuffer;
            _rentedBuffer = null;
            _buffer = default;
            _position = 0;
            if (toReturn != null)
            {
                ArrayPool<char>.Shared.Return(toReturn);
            }
        }

        private void ResizeBuffer(int addLength)
        {
            var newSize = _position + addLength;
            if (newSize <= _buffer.Length)
            {
                return;
            }

            newSize *= 2;

            var rented = ArrayPool<char>.Shared.Rent(newSize);
            _buffer[.._position].CopyTo(rented);

            var previousRented = _rentedBuffer;
            _rentedBuffer = rented;
            _buffer = rented;

            if (previousRented != null)
            {
                ArrayPool<char>.Shared.Return(previousRented);
            }
        }
    }
}
