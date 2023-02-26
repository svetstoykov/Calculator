using Calculator.Application.Common.Result.Models;
using Calculator.Application.Expressions.Interfaces;
using Calculator.Application.Expressions.Models;
using MediatR;

namespace Calculator.Application.Expressions.Commands;

public class Solve
{
    public class Command : IRequest<Result<double>>
    {
        public Command(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; }
    }
    
    public class Handler : IRequestHandler<Command, Result<double>>
    {
        private readonly IEvaluatedExpressionsHistoryService _historyService;
        private readonly IEvaluationService _evaluationService;

        public Handler(IEvaluatedExpressionsHistoryService historyService, IEvaluationService evaluationService)
        {
            _historyService = historyService;
            _evaluationService = evaluationService;
        }

        public async Task<Result<double>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = _evaluationService
                .Calculate(request.Expression);

            await _historyService
                .SaveSolvedExpressionResultAsync(request.Expression, result);
            
            return Result<double>.Success(result);
        }
    }
}