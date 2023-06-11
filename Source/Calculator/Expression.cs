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

                switch (item.Operator)
                {
                    case OperatorType.Begin:
                    {
                        if (_items[i - 1].Type != ComponentType.Operator)
                        {
                            _items.Insert(i, new ExpressionComponent(OperatorType.Mul));
                            i++;
                        }

                        break;
                    }
                    case OperatorType.End:
                    {
                        if (_items[i + 1].Type != ComponentType.Operator)
                        {
                            _items.Insert(i + 1, new ExpressionComponent(OperatorType.Mul));
                            i++;
                        }

                        break;
                    }
                }
            }
        }

        public void ToReversePolishNotation(IErrors errors)
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
                    case ComponentType.Operator:
                    {
                        if (item.Operator == OperatorType.End)
                        {
                            while (stack.Peek().Operator != OperatorType.Begin)
                            {
                                list.Add(stack.Pop());
                            }

                            stack.Pop();
                            break;
                        }

                        if (stack.Count > 0 &&
                            CheckPriority(stack.Peek(), item))
                        {
                            list.Add(stack.Pop());
                        }

                        stack.Push(item);
                        break;
                    }
                }
            }

            if (stack.Count > 0)
            {
                var stackItems = stack.ToArray();
                list.AddRange(stackItems);
            }

            _items.Clear();
            _items.AddRange(list);
        }

        private static bool CheckPriority(
            IExpressionComponent c1,
            IExpressionComponent c2)
        {
            if (c2.Operator == OperatorType.Begin)
            {
                return false;
            }

            var p1 = OperatorsHelper.Priority[c1.Operator];
            var p2 = OperatorsHelper.Priority[c2.Operator];
            return p1 >= p2;
        }

        public override string ToString()
        {
            Span<char> dest = new char[64];
            var sb = new NewStringBuilder();
            
            foreach (var item in _items)
            {
                switch (item.Type)
                {
                    case ComponentType.Value:
                    {
                        if (item.Value.TryFormat(dest, out var length))
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
                }
            }

            return sb.ToString();
        }
    }
}
