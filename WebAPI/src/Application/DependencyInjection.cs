using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register LiteBus modules
        services.AddLiteBus(liteBus =>
        {
            var appAssembly = typeof(DependencyInjection).Assembly;

            // Scan the assembly for all command/query/event handlers
            liteBus.AddCommandModule(module => module.RegisterFromAssembly(appAssembly));
            liteBus.AddQueryModule(module => module.RegisterFromAssembly(appAssembly));
            // liteBus.AddEventModule(module => module.RegisterFromAssembly(appAssembly));
        });

        return services;
    }
}
