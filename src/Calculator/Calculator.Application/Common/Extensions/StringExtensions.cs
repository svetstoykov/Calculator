namespace Calculator.Application.Common.Extensions;

public static class StringExtensions
{
    public static bool IsOpeningParentheses(this string symbol)
        => symbol.First() == '(';

    public static bool IsClosingParentheses(this string symbol)
        => symbol.First() == ')';
}