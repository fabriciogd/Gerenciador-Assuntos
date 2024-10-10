﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Topic.Application.Contracts.Event;
using Topic.BackgroundTasks.Scheduler;
using Topic.BackgroundTasks.Services;
using Topic.Infraestructure.Bus;

namespace Topic.BackgroundTasks.Tasks;

internal sealed class IntegrationEventConsumerBackgroundService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly IJobScheduler _jobScheduler;
    private readonly IScheduler _scheduler;

    public IntegrationEventConsumerBackgroundService(
        ILogger<IntegrationEventConsumerBackgroundService> logger,
        IOptions<MessageBrokerSettings> messageBrokerSettingsOptions,
        IServiceProvider serviceProvider,
        IJobScheduler jobScheduler,
        IJobFactory jobFactory)
    {
        _serviceProvider = serviceProvider;

        _jobScheduler = jobScheduler;
        _scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        _scheduler.JobFactory = jobFactory;

        var messageBrokerSettings = messageBrokerSettingsOptions.Value;

        var factory = new ConnectionFactory
        {
            HostName = messageBrokerSettings.HostName,
            Port = messageBrokerSettings.Port,
            UserName = messageBrokerSettings.UserName,
            Password = messageBrokerSettings.Password
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(messageBrokerSettings.QueueName, false, false, false);

        try
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += OnIntegrationEventReceived;

            _channel.BasicConsume(messageBrokerSettings.QueueName, false, consumer);
        }
        catch (Exception e)
        {
            logger.LogCritical($"ERROR: Failed to process the integration events: {e.Message}", e.Message);
        }

    }

    /// <inheritdoc />
    public Task StartAsync(CancellationToken cancellationToken)
    {
         return _scheduler.Start(cancellationToken);
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        Dispose();

        return _scheduler.Shutdown(cancellationToken);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _channel?.Close();

        _connection?.Close();
    }

    /// <summary>
    /// Processes the integration event received from the message queue.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="eventArgs">The event arguments.</param>
    /// <returns>The completed task.</returns>
    private void OnIntegrationEventReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        string body = Encoding.UTF8.GetString(eventArgs.Body.Span);

        var integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(body, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });

        using IServiceScope scope = _serviceProvider.CreateScope();

        var integrationEventConsumer = scope.ServiceProvider.GetRequiredService<IIntegrationEventConsumer>();

        integrationEventConsumer.Consume(integrationEvent);

        _channel.BasicAck(eventArgs.DeliveryTag, false);
    }
}