using Calculator.Application.Expressions.Models;

namespace Calculator.Application.Expressions.Interfaces;

public interface IEvaluatedExpressionsHistoryService
{
    public Task<ICollection<EvaluatedExpressionHistoryModel>> GetEvaluatedExpressionsHistoryAsync();
    
    public Task<bool> AddEvaluatedExpressionResultToHistoryAsync(string expression, double result);
}