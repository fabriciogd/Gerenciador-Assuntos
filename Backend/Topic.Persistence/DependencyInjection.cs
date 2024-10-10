using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Topic.Application.Contracts.Context;
using Topic.Domain.Repositories;
using Topic.Persistence.Constants;
using Topic.Persistence.Contexts;
using Topic.Persistence.Repositories;

namespace Topic.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(ConnectionString.Default)!;

        services.AddDbContext<TopicDbContext>(options => options.UseNpgsql(connectionString, options =>
        {
            options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                null
            );
        }));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<INewsletterRepository, NewsletterRepository>();

        return services;
    }
}
