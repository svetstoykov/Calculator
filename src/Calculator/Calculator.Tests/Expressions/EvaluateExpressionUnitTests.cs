using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Services;
using NUnit.Framework;

namespace Calculator.Tests.Expressions;

public class EvaluateExpressionUnitTests
{
    private IEvaluationService _evaluationService = null!;
    
    [SetUp]
    public void SetUp()
    {
        _evaluationService = new EvaluationService();
    }
    
    [Test]
    public void EvaluateExpression_WithValidData_ReturnsResult()
    {
        const string expression = "(-81 / 9  * (2 + 11) * 31) - 99   /  9 +  (   ( -100  / 20) * 20 ) / 10";

        var result = _evaluationService.Evaluate(expression);

        const double expectedResult = (-81 / 9 * (2 + 11) * 31) - 99 / 9 + ((-100 / 20) * 20) / 10;
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void EvaluateExpression_WithInvalidSymbols_ThrowsArgumentException()
    {
        const string expression = "(22 + 1) * 2 + (31a + b)";

        Assert.Throws<ArgumentException>(() => _evaluationService.Evaluate(expression));
    }

    [Test]
    public void EvaluateExpression_WithInvalidParentheses_ThrowsArgumentException()
    {
        const string expression = "(22 + 1)) * 2 + (99 / 77)";

        Assert.Throws<ArgumentException>(() => _evaluationService.Evaluate(expression));
    }
    
    [Test]
    public void EvaluateExpression_WithEmptyExpression_ThrowsArgumentException()
    {
        const string expression = "";

        Assert.Throws<ArgumentException>(() => _evaluationService.Evaluate(expression));
    }
}