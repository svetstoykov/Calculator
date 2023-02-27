using Calculator.Application.Expressions.Models;

namespace Calculator.Application.Expressions.Interfaces;

public interface IEvaluatedExpressionsHistoryService
{
    public Task<ICollection<EvaluatedExpressionHistoryModel>> GetOrCreateEvaluatedExpressionsHistoryAsync();
    
    public Task<bool> SaveEvaluatedExpressionResultAsync(string expression, double result);
}