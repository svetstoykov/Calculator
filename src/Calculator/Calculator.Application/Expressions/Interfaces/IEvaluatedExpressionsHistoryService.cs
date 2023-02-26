using Calculator.Application.Expressions.Models;

namespace Calculator.Application.Expressions.Interfaces;

public interface IEvaluatedExpressionsHistoryService
{
    public Task<ICollection<ExpressionHistoryModel>> GetOrCreateExpressionSolveHistoryAsync();
    
    public Task<bool> SaveSolvedExpressionResultAsync(string expression, double result);
}