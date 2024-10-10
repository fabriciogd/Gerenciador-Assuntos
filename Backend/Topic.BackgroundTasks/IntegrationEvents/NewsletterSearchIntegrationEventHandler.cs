using MediatR;
using Topic.Application.UseCases.Newsletters.IntegrationEvents;
using Topic.BackgroundTasks.Scheduler;
using Topic.Domain.Enums;

namespace Topic.BackgroundTasks.IntegrationEvents;

internal sealed class NewsletterSearchIntegrationEventHandler(IJobScheduler _jobScheduler) : INotificationHandler<NewsletterSearchIntegrationEvent>
{
    public async Task Handle(NewsletterSearchIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Status == StatusEnum.InProgress)
        {
            await _jobScheduler.AddJob(notification.Id);
            return;
        }

        await _jobScheduler.RemoveJob(notification.Id);
    }
}
