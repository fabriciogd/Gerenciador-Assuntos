using MediatR;
using Topic.Application.Contracts.Event;

namespace Topic.BackgroundTasks.Services;

internal sealed class IntegrationEventConsumer : IIntegrationEventConsumer
{
    private readonly IMediator _mediator;

    public IntegrationEventConsumer(IMediator mediator) => _mediator = mediator;

    public void Consume(IIntegrationEvent integrationEvent) => _mediator.Publish(integrationEvent).GetAwaiter().GetResult();
}