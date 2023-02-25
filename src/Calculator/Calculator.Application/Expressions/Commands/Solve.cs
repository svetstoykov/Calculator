using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Models;
using MediatR;

namespace Calculator.Application.Expressions.Commands;

public class Solve
{
    public class Command : IRequest<bool>
    {
        public Command(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; }
    }
    
    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IEvaluatedExpressionsHistoryService _historyService;
        private readonly IEvaluationService _evaluationService;

        public Handler(IEvaluatedExpressionsHistoryService historyService, IEvaluationService evaluationService)
        {
            _historyService = historyService;
            _evaluationService = evaluationService;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = _evaluationService
                .Calculate(request.Expression);

            var history = await _historyService
                .GetOrCreateExpressionSolveHistoryAsync();

            history.Add(new ExpressionHistoryModel
            {
                Expression = request.Expression,
                Result = result,
                DateCreated = DateTime.UtcNow
            });
            
            return true;
        }
    }
}