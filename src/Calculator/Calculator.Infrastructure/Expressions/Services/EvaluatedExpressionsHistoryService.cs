using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Models;
using Calculator.Application.Expressions.Models.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Calculator.Infrastructure.Expressions.Services;

public class EvaluatedExpressionsHistoryService : IEvaluatedExpressionsHistoryService
{
    private readonly IMemoryCache _cache;
    private readonly ExpressionsConfiguration _expressionsConfiguration;

    private const string CacheKey = nameof(EvaluatedExpressionsHistoryService);

    public EvaluatedExpressionsHistoryService(
        IMemoryCache cache,
        IOptionsMonitor<ExpressionsConfiguration> expressionsConfigOptionsMonitor)
    {
        _cache = cache;
        _expressionsConfiguration = expressionsConfigOptionsMonitor.CurrentValue;
    }

    public async Task<ICollection<EvaluatedExpressionHistoryModel>> GetEvaluatedExpressionsHistoryAsync()
    {
        var history = GetOrCreateHistoryFromCache();

        return await Task.FromResult(history);
    }
    
    public async Task<bool> AddEvaluatedExpressionResultToHistoryAsync(string expression, double result)
    {
        var history = GetOrCreateHistoryFromCache();

        history.Add(new EvaluatedExpressionHistoryModel
        {
            Expression = expression,
            Result = result,
            DateCreated = DateTime.Now
        });
        
        return await Task.FromResult(true);
    }

    private ICollection<EvaluatedExpressionHistoryModel> GetOrCreateHistoryFromCache()
    {
        var cacheExpirationInSeconds = _expressionsConfiguration
            .HistoryCacheExpirationTimeInSeconds;

        var history = _cache.GetOrCreate<ICollection<EvaluatedExpressionHistoryModel>>(
            CacheKey, cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan
                    .FromSeconds(cacheExpirationInSeconds);

                return new List<EvaluatedExpressionHistoryModel>();
            });
        
        return history!;
    }
}