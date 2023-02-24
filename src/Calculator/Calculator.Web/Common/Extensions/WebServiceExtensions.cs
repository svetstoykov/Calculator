using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Calculator.Web.Common.Extensions;

public static class WebServiceExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services
            .AddSwaggerGen()
            .AddFluentValidation()
            .AddControllers();

        return services;
    }
    
    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        => services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}