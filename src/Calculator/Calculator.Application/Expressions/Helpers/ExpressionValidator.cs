using System.Text.RegularExpressions;
using Calculator.Application.Common.Extensions;

namespace Calculator.Application.Expressions.Helpers;

public static class ExpressionValidator
{
    private static class ErrorMessage
    {
        public const string ForExpressionContainingInvalidParenthesis
            = "Invalid Parenthesis";
        
        public const string ForExpressionContainingInvalidSymbols
            = "Expression can only contain: [0-9, *, /, +, -, (, )]";
        
        public const string ForExpressionIsNullOrEmpty
            = "Expression is empty";
    }

    public static void Validate(string expression)
    {
        ValidateIsNotNullOrEmpty(expression);
        ValidateAllowedSymbols(expression);
        ValidateParentheses(expression);
    }

    private static void ValidateIsNotNullOrEmpty(string expression)
    {
        if (string.IsNullOrEmpty(expression))
        {
            throw new ArgumentException(
                ErrorMessage.ForExpressionIsNullOrEmpty);
        }
    }

    private static void ValidateAllowedSymbols(string expression)
    {
        const string pattern = @"^[0-9\+\-\*\(\)\/\s]*$";
        if (!Regex.IsMatch(expression, pattern))
        {
            throw new ArgumentException(
                ErrorMessage.ForExpressionContainingInvalidSymbols);
        }
    }
    
    private static void ValidateParentheses(string expression)
    {
        var stack = new Stack<string>();
        var allParentheses = expression
            .Where(c => c is '(' or ')')
            .Select(c => c.ToString())
            .ToList();
        
        foreach (var parentheses in allParentheses)
        {
            if (parentheses.IsClosingParenthesis())
            {
                if (!stack.TryPop(out _))
                {
                    throw new ArgumentException(
                        ErrorMessage.ForExpressionContainingInvalidParenthesis);
                }
                
                continue;
            }
            
            stack.Push(parentheses);
        }

        if (stack.Any())
        {
            throw new ArgumentException(
                ErrorMessage.ForExpressionContainingInvalidParenthesis);
        }
    }
}