namespace Topic.BackgroundTasks.Scheduler;

internal interface IJobScheduler
{
    Task AddJob(Guid newsletterId);

    Task RemoveJob(Guid newsletterId);
}
