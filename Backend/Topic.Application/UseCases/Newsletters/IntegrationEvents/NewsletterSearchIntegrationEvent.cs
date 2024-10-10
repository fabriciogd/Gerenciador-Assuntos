using Topic.Application.Contracts.Event;

namespace Topic.Application.UseCases.Newsletters.IntegrationEvents;
public sealed record NewsletterSearchIntegrationEvent(Guid Id, string Operation): IIntegrationEvent;