using System.Text.RegularExpressions;
using Calculator.Application.Common.Extensions;

namespace Calculator.Application.Expressions.Helpers;

public static class ExpressionValidator
{
    private static class ErrorMessage
    {
        public const string ForInvalidParenthesis
            = "Invalid Parenthesis";
        
        public const string ForInvalidSymbols
            = "Expression can only contain: [0-9, *, /, +, -, (, )]";
    }

    public static void Validate(string expression)
    {
        ValidateSymbols(expression);
        ValidateParentheses(expression);
    }
    
    private static void ValidateSymbols(string expression)
    {
        const string pattern = @"^[0-9\+\-\*\(\)\/\s]*$";
        if (!Regex.IsMatch(expression, pattern))
        {
            throw new ArgumentException(
                ErrorMessage.ForInvalidSymbols);
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
                        ErrorMessage.ForInvalidParenthesis);
                }
                
                continue;
            }
            
            stack.Push(parentheses);
        }

        if (stack.Any())
        {
            throw new ArgumentException(
                ErrorMessage.ForInvalidParenthesis);
        }
    }
}