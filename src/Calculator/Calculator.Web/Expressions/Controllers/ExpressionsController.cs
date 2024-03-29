﻿using AutoMapper;
using Calculator.Application.Expressions.Commands;
using Calculator.Application.Expressions.Queries;
using Calculator.Web.Common.Controllers;
using Calculator.Web.Expressions.Models.Request;
using Calculator.Web.Expressions.Models.Response.History;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Web.Expressions.Controllers;

public class ExpressionsController : BaseApiController
{
    public ExpressionsController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var history = await Mediator.Send(new GetEvaluatedExpressionsHistory.Query());

        var viewModel = new ExpressionHistoryViewModel
        {
            EvaluatedExpressionsHistory = Mapper.Map<IEnumerable<EvaluatedExpressionHistoryViewModel>>(history.Data)
        };

        return View("~/Expressions/Views/Index.cshtml", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Evaluate(ExpressionRequestModel requestModel)
        => HandleResult(await Mediator.Send(new Evaluate.Command(requestModel.Expression)));
}