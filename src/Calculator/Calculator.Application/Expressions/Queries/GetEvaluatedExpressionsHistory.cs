using Calculator.Application.Common.Result.Models;
using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Models;
using MediatR;

namespace Calculator.Application.Expressions.Queries;

public class GetEvaluatedExpressionsHistory
{
    public class Query : IRequest<Result<IEnumerable<EvaluatedExpressionHistoryModel>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<IEnumerable<EvaluatedExpressionHistoryModel>>>
    {
        private readonly IEvaluatedExpressionsHistoryService _historyService;

        public Handler(IEvaluatedExpressionsHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<Result<IEnumerable<EvaluatedExpressionHistoryModel>>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var history = (await _historyService
                    .GetOrCreateEvaluatedExpressionsHistoryAsync())
                .OrderByDescending(h => h.DateCreated);

            return Result<IEnumerable<EvaluatedExpressionHistoryModel>>
                .Success(history);
        }
    }
}