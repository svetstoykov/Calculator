using Calculator.Application.Expressions.Commands;
using Calculator.Application.Expressions.Queries;
using Calculator.Web.Common.Controllers;
using Calculator.Web.Expressions.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Web.Expressions.Controllers;

public class ExpressionsController : BaseApiController
{
    public ExpressionsController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpGet]
    public IActionResult Index()
        => View("~/Expressions/Views/Index.cshtml");

    [HttpPost]
    public async Task<IActionResult> Solve(ExpressionRequestModel requestModel)
        => Ok(await Mediator.Send(new Solve.Command(requestModel.Expression)));

    [HttpGet]
    public async Task<IActionResult> GetSolvedExpressionsHistory()
        => Ok(await Mediator.Send(new GetSolvedExpressionsHistory.Query()));
}