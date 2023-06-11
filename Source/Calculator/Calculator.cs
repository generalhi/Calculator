using Calculator.Entities;
using Calculator.Enums;
using Calculator.Extensions;
using Calculator.Helpers;
using Calculator.Interfaces;

namespace Calculator
{
    public class Calculator
    {
        public float? Run(string text, IParser parser, IErrors errors)
        {
            errors.Clear();

            var expression = parser.Parse(text, errors);
            if (errors.IsPresent)
            {
                return null;
            }

#if DEBUG
            Console.WriteLine(text);
            Console.WriteLine(expression.ToString());
#endif
            expression.ToReversePolishNotation(errors);
#if DEBUG
            Console.WriteLine(expression.ToString());
            Console.WriteLine("Operations:");
#endif
            return CalcExpression(expression);
        }

        private float CalcExpression(IExpression expression)
        {
            var stack = new Stack<IExpressionComponent>();
            var n = 1;
            for (var i = 0; i < expression.Count; i++)
            {
                var c = expression[i];
                switch (c.Type)
                {
                    case ComponentType.Value:
                    {
                        stack.Push(c);
                        break;
                    }
                    case ComponentType.Operator:
                    {
                        var c2 = stack.Pop();
                        var c1 = stack.Pop();
                        var result = OperatorsHelper.Function[c.Operator](c1.Value, c2.Value);
                        stack.Push(new ExpressionComponent(result));
#if DEBUG
                        Console.WriteLine($"{n++}) {c1.Value}{c.Operator.ToChar()}{c2.Value}={result}");
#endif
                        break;
                    }
                }
            }
            return stack.Peek().Value;
        }
    }
}
