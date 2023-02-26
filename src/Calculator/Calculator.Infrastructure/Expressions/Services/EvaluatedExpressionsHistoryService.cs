﻿using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Models;
using Calculator.Application.Expressions.Models.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Calculator.Infrastructure.Expressions.Services;

public class EvaluatedExpressionsHistoryService : IEvaluatedExpressionsHistoryService
{
    private readonly IMemoryCache _cache;
    private readonly IOptionsMonitor<ExpressionsConfiguration> _configurationOptionsMonitor;

    private const string CacheKey = nameof(EvaluatedExpressionsHistoryService);

    public EvaluatedExpressionsHistoryService(
        IMemoryCache cache,
        IOptionsMonitor<ExpressionsConfiguration> configurationOptionsMonitor)
    {
        _cache = cache;
        _configurationOptionsMonitor = configurationOptionsMonitor;
    }

    public async Task<ICollection<ExpressionHistoryModel>> GetOrCreateExpressionSolveHistoryAsync()
    {
        var history = GetOrCreateHistoryFromCache();

        return await Task.FromResult(history!);
    }
    
    public async Task<bool> SaveSolvedExpressionResultAsync(string expression, double result)
    {
        var history = GetOrCreateHistoryFromCache();

        history.Add(new ExpressionHistoryModel
        {
            Expression = expression,
            Result = result,
            DateCreated = DateTime.Now
        });
        
        return await Task.FromResult(true);
    }

    private ICollection<ExpressionHistoryModel> GetOrCreateHistoryFromCache()
    {
        var cacheExpirationInSeconds = _configurationOptionsMonitor
            .CurrentValue.CacheExpirationTimeInSeconds;

        var history = _cache.GetOrCreate<ICollection<ExpressionHistoryModel>>(
            CacheKey, cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan
                    .FromSeconds(cacheExpirationInSeconds);

                return new List<ExpressionHistoryModel>();
            });
        
        return history!;
    }
}