using System.Globalization;
using Calculator.Entities;
using Calculator.Enums;
using Calculator.Extensions;
using Calculator.Helpers;
using Calculator.Interfaces;

namespace Calculator
{
    public class Expression : IExpression
    {
        private readonly List<IExpressionComponent> _items = new List<IExpressionComponent>();

        public int Count => _items.Count;

        public IExpressionComponent this[int index] => _items[index];

        public void Add(IExpressionComponent item)
        {
            _items.Add(item);
        }

        public void Normalize()
        {
            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];

                if (i == 0 ||
                    i == _items.Count - 1)
                {
                    continue;
                }

                if (item.Type != ComponentType.Bracket)
                {
                    continue;
                }

                if (item.Bracket == BracketType.Open &&
                    _items[i - 1].Type == ComponentType.Value)
                {
                    _items.Insert(i, new ExpressionComponent(OperatorType.Mul));
                    i++;
                }
                else if (item.Bracket == BracketType.Close &&
                         _items[i + 1].Type == ComponentType.Value)
                {
                    _items.Insert(i + 1, new ExpressionComponent(OperatorType.Mul));
                    i++;
                }
            }
        }

        public void ToReversePolishNotation()
        {
            var list = new List<IExpressionComponent>(_items.Count);
            var stack = new Stack<IExpressionComponent>(_items.Count);

            foreach (var item in _items)
            {
                switch (item.Type)
                {
                    case ComponentType.Value:
                    {
                        list.Add(item);
                        break;
                    }
                    case ComponentType.Bracket:
                    {
                        if (item.Bracket == BracketType.Open)
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            while (stack.Peek().Type != ComponentType.Bracket)
                            {
                                list.Add(stack.Pop());
                            }

                            stack.Pop();
                        }

                        break;
                    }
                    case ComponentType.Operator:
                    {
                        if (stack.Count > 0 &&
                            stack.Peek().Type == ComponentType.Operator &&
                            OperatorsHelper.Priority[stack.Peek().Operator] >= OperatorsHelper.Priority[item.Operator])
                        {
                            list.Add(stack.Pop());
                        }

                        stack.Push(item);
                        break;
                    }
                }
            }

            while (stack.Count > 0)
            {
                list.Add(stack.Pop());
            }

            _items.Clear();
            _items.AddRange(list);
        }

        public override string ToString()
        {
            Span<char> dest = stackalloc char[64];
            using var sb = new NewStringBuilder();

            foreach (var item in _items)
            {
                switch (item.Type)
                {
                    case ComponentType.Value:
                    {
                        if (item.Value.TryFormat(dest, out var length, default, CultureInfo.InvariantCulture))
                        {
                            sb.Append(dest.Slice(0, length));
                        }

                        break;
                    }
                    case ComponentType.Operator:
                    {
                        sb.Append(item.Operator.ToChar());
                        break;
                    }
                    case ComponentType.Bracket:
                    {
                        sb.Append(item.Bracket.ToChar());
                        break;
                    }
                }
            }

            return sb.ToString();
        }
    }
}
