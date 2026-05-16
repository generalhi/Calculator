using System.Globalization;
using Calculator.Entities;
using Calculator.Enums;
using Calculator.Extensions;
using Calculator.Helpers;
using Calculator.Interfaces;

namespace Calculator
{
    public class Parser : IParser
    {
        public IExpression Parse(string text, IErrors errors)
        {
            IExpression expression = new Expression();

            if (string.IsNullOrEmpty(text))
            {
                errors.Add("Math expression text not found!");
                return expression;
            }

            var startIndex = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (OperatorsHelper.Possible.Contains(text[i]))
                {
                    if (startIndex != i)
                    {
                        ParseFloatSubString(text, startIndex, i - startIndex, expression, errors);
                    }

                    ParseMathOperator(text, i, expression);
                    startIndex = i + 1;

                    if (expression.Count > 1 &&
                        expression[^2].Type == ComponentType.Operator &&
                        expression[^1].Type == ComponentType.Operator &&
                        expression[^2].Operator != OperatorType.Begin &&
                        expression[^2].Operator != OperatorType.End &&
                        expression[^1].Operator != OperatorType.Begin &&
                        expression[^1].Operator != OperatorType.End)
                    {
                        errors.Add(
                            $"Error: Double operator '{expression[^2].Operator.ToChar()}{expression[^1].Operator.ToChar()}'.");
                        return expression;
                    }

                    continue;
                }

                if (i == text.Length - 1)
                {
                    ParseFloatSubString(text, startIndex, i - startIndex + 1, expression, errors);
                }
            }

            ValidateExpression(expression, errors);
            if (errors.IsPresent)
            {
                return expression;
            }

            expression.Normalize();

            return expression;
        }

        private static void ValidateExpression(IExpression expression, IErrors errors)
        {
            if (expression.Count == 0)
            {
                return;
            }

            var first = expression[0];
            if (first.Type == ComponentType.Operator && first.Operator != OperatorType.Begin)
            {
                errors.Add($"Error: Expression starts with operator '{first.Operator.ToChar()}'.");
                return;
            }

            var last = expression[^1];
            if (last.Type == ComponentType.Operator && last.Operator != OperatorType.End)
            {
                errors.Add($"Error: Expression ends with operator '{last.Operator.ToChar()}'.");
                return;
            }

            var balance = 0;
            for (var i = 0; i < expression.Count; i++)
            {
                var c = expression[i];
                if (c.Type != ComponentType.Operator)
                {
                    continue;
                }

                if (c.Operator == OperatorType.Begin)
                {
                    balance++;
                    if (i + 1 < expression.Count &&
                        expression[i + 1].Type == ComponentType.Operator &&
                        expression[i + 1].Operator == OperatorType.End)
                    {
                        errors.Add("Error: Empty parentheses.");
                        return;
                    }
                }
                else if (c.Operator == OperatorType.End)
                {
                    balance--;
                    if (balance < 0)
                    {
                        errors.Add("Error: Unbalanced parentheses.");
                        return;
                    }
                }
            }

            if (balance != 0)
            {
                errors.Add("Error: Unbalanced parentheses.");
            }
        }

        private void ParseFloatSubString(
            string text,
            int startIndex,
            int length,
            IExpression expression,
            IErrors errors)
        {
            var subString = text.AsSpan(startIndex, length);
            if (float.TryParse(subString, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
            {
                expression.Add(new ExpressionComponent(val));
            }
            else
            {
                errors.Add($"Error: '{subString}' not parsed.");
            }
        }

        private void ParseMathOperator(string text, int index, IExpression expression)
        {
            var mathOperationType = text[index].ToOperator();
            expression.Add(new ExpressionComponent(mathOperationType));
        }
    }
}
