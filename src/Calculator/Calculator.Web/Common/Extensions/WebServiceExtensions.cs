using System.Reflection;
using Calculator.Application.Expressions.Models.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;

namespace Calculator.Web.Common.Extensions;

public static class WebServiceExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMemoryCache()
            .AddOptionsMonitors(configuration)
            .AddSwaggerGen()
            .AddFluentValidation()
            .AddControllers();
        
        return services;
    }

    private static IServiceCollection AddOptionsMonitors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExpressionsConfiguration>()
            .Bind(configuration.GetSection(nameof(ExpressionsConfiguration)));

        return services;
    }

    
    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        => services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}