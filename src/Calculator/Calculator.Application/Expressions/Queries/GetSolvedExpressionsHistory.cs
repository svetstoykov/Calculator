using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Models;
using MediatR;

namespace Calculator.Application.Expressions.Queries;

public class GetSolvedExpressionsHistory
{
    public class Query : IRequest<IEnumerable<ExpressionHistoryModel>>
    { }
    
    public class Handler : IRequestHandler<Query, IEnumerable<ExpressionHistoryModel>>
    {
        private readonly IEvaluatedExpressionsHistoryService _historyService;
        
        public Handler(IEvaluatedExpressionsHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<IEnumerable<ExpressionHistoryModel>> Handle(Query request, CancellationToken cancellationToken) 
            => await _historyService
                .GetOrCreateExpressionSolveHistoryAsync();
    }
}