using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.Infrastructure.Common.InfrastructureServices;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        => services
            .AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());
}