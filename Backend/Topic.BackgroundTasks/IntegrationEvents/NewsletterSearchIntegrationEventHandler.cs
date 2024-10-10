using MediatR;
using Topic.Application.UseCases.Newsletters.IntegrationEvents;
using Topic.BackgroundTasks.Scheduler;

namespace Topic.BackgroundTasks.IntegrationEvents;

internal sealed class NewsletterSearchIntegrationEventHandler(IJobScheduler _jobScheduler) : INotificationHandler<NewsletterSearchIntegrationEvent>
{
    public async Task Handle(NewsletterSearchIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Operation == "Add")
        {
            await _jobScheduler.AddJob(notification.Id);
            return;
        }

        await _jobScheduler.RemoveJob(notification.Id);
    }
}
