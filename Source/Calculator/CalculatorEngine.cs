using Calculator.Entities;
using Calculator.Enums;
using Calculator.Extensions;
using Calculator.Helpers;
using Calculator.Interfaces;

namespace Calculator
{
    public class CalculatorEngine
    {
        public decimal? Run(string text, IParser parser, IErrors errors)
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
            expression.ToReversePolishNotation();
#if DEBUG
            Console.WriteLine(expression.ToString());
            Console.WriteLine("Operations:");
#endif
            return CalcExpression(expression, errors);
        }

        private static decimal? CalcExpression(IExpression expression, IErrors errors)
        {
            var stack = new Stack<IExpressionComponent>();
#if DEBUG
            var n = 1;
#endif
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
                        if (stack.Count < 2)
                        {
                            errors.Add($"Error: Missing operand for '{c.Operator.ToChar()}'.");
                            return null;
                        }

                        var c2 = stack.Pop();
                        var c1 = stack.Pop();
                        if (c.Operator == OperatorType.Div && c2.Value == 0m)
                        {
                            errors.Add("Error: Division by zero.");
                            return null;
                        }

                        var result = OperatorsHelper.Function[c.Operator](c1.Value, c2.Value);
                        stack.Push(new ExpressionComponent(result));
#if DEBUG
                        Console.WriteLine(FormattableString.Invariant(
                            $"{n++}) {c1.Value}{c.Operator.ToChar()}{c2.Value}={result}"));
#endif
                        break;
                    }
                }
            }

            if (stack.Count == 0)
            {
                errors.Add("Error: Invalid expression.");
                return null;
            }

            return stack.Peek().Value;
        }
    }
}
