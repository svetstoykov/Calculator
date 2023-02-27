using Calculator.Application.Expressions.Services;
using NUnit.Framework;

namespace Calculator.Tests.Expressions;

public class EvaluateExpressionUnitTests
{
    [Test]
    public void EvaluateExpression_WithValidData_ReturnsResult()
    {
        const string expression = "(-81 / 9  * (2 + 11) * 31) - 99   /  9 +  (   ( -100  / 20) * 20 ) / 10";
        var service = new EvaluationService();

        var result = service.Evaluate(expression);

        const double expectedResult = (-81 / 9 * (2 + 11) * 31) - 99 / 9 + ((-100 / 20) * 20) / 10;
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void EvaluateExpression_WithInvalidSymbols_ThrowsArgumentException()
    {
        const string expression = "(22 + 1) * 2 + (31a + b)";
        var service = new EvaluationService();

        Assert.Throws<ArgumentException>(() => service.Evaluate(expression));
    }

    [Test]
    public void EvaluateExpression_WithInvalidParentheses_ThrowsArgumentException()
    {
        const string expression = "(22 + 1)) * 2 + (99 / 77)";
        var service = new EvaluationService();

        Assert.Throws<ArgumentException>(() => service.Evaluate(expression));
    }
    
    [Test]
    public void EvaluateExpression_WithEmptyExpression_ThrowsArgumentException()
    {
        const string expression = "";
        var service = new EvaluationService();

        Assert.Throws<ArgumentException>(() => service.Evaluate(expression));
    }
}