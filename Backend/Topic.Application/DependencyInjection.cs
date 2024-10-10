using Microsoft.Extensions.DependencyInjection;
namespace Topic.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            });

        return services;
    }
}
