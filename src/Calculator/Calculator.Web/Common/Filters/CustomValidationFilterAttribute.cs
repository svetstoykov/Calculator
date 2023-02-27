using System.Net;
using Calculator.Application.Common.Result.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Calculator.Web.Common.Filters;

public class CustomValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        
        var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage)
            .ToList();

        var responseObj = Result<object>.Failure(string.Join("!", errors));

        context.Result = new JsonResult(responseObj)
        {
            StatusCode = (int) HttpStatusCode.BadRequest
        };
    }
}