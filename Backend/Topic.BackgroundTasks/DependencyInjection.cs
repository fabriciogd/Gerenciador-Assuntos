using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz;
using Topic.BackgroundTasks.Scheduler;
using Topic.BackgroundTasks.Services;
using Topic.BackgroundTasks.Tasks;
using Topic.BackgroundTasks.Job;
using Quartz.Spi;

namespace Topic.BackgroundTasks;
public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        services.AddHostedService<IntegrationEventConsumerBackgroundService>();
        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();

        services.AddSingleton<IJobFactory, JobFactory>();
        services.AddSingleton<IJobScheduler, JobScheduler>();
        services.AddSingleton<IScheduler>(provider => StdSchedulerFactory.GetDefaultScheduler().Result);

        services.AddTransient<SyncNewsJob>();

        return services;
    }
}