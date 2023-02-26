using Calculator.Application.Common.Result.Models;
using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Models;
using MediatR;

namespace Calculator.Application.Expressions.Queries;

public class GetSolvedExpressionsHistory
{
    public class Query : IRequest<Result<IEnumerable<ExpressionHistoryModel>>>
    { }
    
    public class Handler : IRequestHandler<Query, Result<IEnumerable<ExpressionHistoryModel>>>
    {
        private readonly IEvaluatedExpressionsHistoryService _historyService;
        
        public Handler(IEvaluatedExpressionsHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<Result<IEnumerable<ExpressionHistoryModel>>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var history = (await _historyService
                .GetOrCreateExpressionSolveHistoryAsync())
                .OrderByDescending(h => h.DateCreated);

            return Result<IEnumerable<ExpressionHistoryModel>>
                .Success(history);
        }
    }
}