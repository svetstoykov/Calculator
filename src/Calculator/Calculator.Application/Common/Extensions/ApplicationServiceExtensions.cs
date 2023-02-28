using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.Application.Common.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());
}