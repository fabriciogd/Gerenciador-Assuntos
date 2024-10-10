using Topic.Application.Contracts.Event;

namespace Topic.BackgroundTasks.Services;

internal interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}