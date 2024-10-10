using Topic.Application.Contracts.Event;
using Topic.Domain.Enums;

namespace Topic.Application.UseCases.Newsletters.IntegrationEvents;
public sealed record NewsletterSearchIntegrationEvent(Guid Id, StatusEnum Status): IIntegrationEvent;