namespace Calculator.Application.Common.Extensions;

public static class StringExtensions
{
    public static bool IsOpeningParenthesis(this string symbol)
        => symbol.First() == '(';

    public static bool IsClosingParenthesis(this string symbol)
        => symbol.First() == ')';
}