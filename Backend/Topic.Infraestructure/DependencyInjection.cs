using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using Topic.Application.Contracts.Bus;
using Topic.Application.Contracts.News;
using Topic.Infraestructure.Bus;
using Topic.Infraestructure.Handlers;
using Topic.Infraestructure.News;

namespace Topic.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SettingsKey));
        services.Configure<NewsSettings>(configuration.GetSection(NewsSettings.SettingsKey));

        services.AddScoped<IEventPublisher, RabbitMQEventPublisher>();

        services.AddHttpClient("News", (serviceProvider, client) =>
        {
            var settings = serviceProvider
                .GetRequiredService<IOptions<NewsSettings>>().Value;

            client.BaseAddress = new Uri(settings.Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        })
        .AddHttpMessageHandler<ApiKeyDelegatingHandler>();

        services.AddScoped<ApiKeyDelegatingHandler>();
        services.AddScoped<INewsService, NewsService>();

        return services;
    }
}
