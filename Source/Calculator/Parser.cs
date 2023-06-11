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
                        errors.Add($"Error: Double operator '{expression[^2].Operator.ToChar()}{expression[^1].Operator.ToChar()}'.");
                        return expression;
                    }

                    continue;
                }

                if (i == text.Length - 1)
                {
                    ParseFloatSubString(text, startIndex, i - startIndex + 1, expression, errors);
                }
            }

            expression.Normalize();

            return expression;
        }

        private void ParseFloatSubString(
            string text,
            int startIndex,
            int length,
            IExpression expression,
            IErrors errors)
        {
            var subString = text.AsSpan(startIndex, length);
            if (float.TryParse(subString, out var val))
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
