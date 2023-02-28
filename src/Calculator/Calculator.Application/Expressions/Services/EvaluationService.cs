using System.Globalization;
using System.Text;
using Calculator.Application.Common.Extensions;
using Calculator.Application.Expressions.Helpers;
using Calculator.Application.Expressions.Interfaces;

namespace Calculator.Application.Expressions.Services;

public class EvaluationService : IEvaluationService
{
    public double Evaluate(string expression)
    {
        ExpressionValidator.Validate(expression);
        
        var operatorsAndNumbers = SplitExpressionIntoOperatorsAndNumbers(expression)
            .ToList();

        var (result, _) = EvaluateOperatorsAndNumbers(operatorsAndNumbers, default);

        return result;
    }

    private static ValueTuple<double, int> EvaluateOperatorsAndNumbers(IList<string> operatorsAndNumbers, int index)
    {
        var outerExpression = new List<string>();
        for (var i = index; i < operatorsAndNumbers.Count; i++)
        {
            var currentItem = operatorsAndNumbers[i];
            if (currentItem.IsOpeningParenthesis())
            {
                var (innerExpressionValue, advancedIndex)
                    = EvaluateOperatorsAndNumbers(operatorsAndNumbers, ++i);

                outerExpression.Add(innerExpressionValue.ToString(CultureInfo.InvariantCulture));

                i = advancedIndex;

                continue;
            }

            if (currentItem.IsClosingParenthesis())
            {
                return (EvaluateBasicExpression(outerExpression), i);
            }

            outerExpression.Add(currentItem);
        }

        return (EvaluateBasicExpression(outerExpression), default);
    }

    private static double EvaluateBasicExpression(IList<string> operatorsAndNumbers)
    {
        if (!operatorsAndNumbers.Any())
        {
            return 0;
        }

        var stack = new Stack<double>();
        var @operator = '+';

        double result = 0;
        double number = 0;
        for (var i = 0; i < operatorsAndNumbers.Count; i++)
        {
            var entry = operatorsAndNumbers[i];
            var isDigit = double.TryParse(entry, out var tempNum);
            if (isDigit)
            {
                number = tempNum;
            }

            if (!isDigit || i == operatorsAndNumbers.Count - 1)
            {
                switch (@operator)
                {
                    case '+':
                        stack.Push(number);
                        break;
                    case '-':
                        stack.Push(-number);
                        break;
                    case '*':
                        stack.Push(stack.Pop() * number);
                        break;
                    case '/':
                        stack.Push(stack.Pop() / number);
                        break;
                }

                number = 0;
                @operator = entry.First();
            }
        }

        while (stack.Any())
        {
            result += stack.Pop();
        }

        return result;
    }

    private static IEnumerable<string> SplitExpressionIntoOperatorsAndNumbers(string expression)
    {
        var result = new List<string>();
        for (var i = 0; i < expression.Length; i++)
        {
            var currentSymbol = expression[i];
            if (currentSymbol == ' ')
            {
                continue;
            }

            if (char.IsDigit(currentSymbol))
            {
                var numBuilder = new StringBuilder();
                numBuilder.Append(currentSymbol);

                while (i + 1 < expression.Length && char.IsDigit(expression[i + 1]))
                {
                    numBuilder.Append(expression[++i]);
                }

                result.Add(numBuilder.ToString());

                continue;
            }

            result.Add(currentSymbol.ToString());
        }

        return result;
    }
}