using System.Reflection;
using Calculator.Application.Expressions.Models.Settings;
using Calculator.Web.Common.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Web.Common.Extensions;

public static class WebServiceExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddCustomValidationFilterAttribute()
            .AddMemoryCache()
            .AddOptionsMonitors(configuration)
            .AddSwaggerGen()
            .AddFluentValidation()
            .AddMvc()
            .AddRazorRuntimeCompilation();

        services.AddControllersWithViews();
        
        return services;
    }
    
    private static IServiceCollection AddCustomValidationFilterAttribute(this IServiceCollection services)
    {
        services.AddMvc(cfg => { cfg.Filters.Add<CustomValidationFilterAttribute>(); });

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

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