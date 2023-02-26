using System.Net;
using System.Text.Json;
using Calculator.Application.Common.Result.Models;

namespace Calculator.Web.Common.Middleware.ErrorHandling;

public class ErrorHandlerMiddleware
{
    private class ErrorMessage
    {
        public const string FromCommonError = "Something went wrong";
    }
    
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;

    public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = ex switch
            {
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var exceptionResponse = Result<object>.Failure(string.IsNullOrEmpty(ex.Message)
                ? ErrorMessage.FromCommonError
                : ex.Message);

            await response.WriteAsync(JsonSerializer.Serialize(exceptionResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}