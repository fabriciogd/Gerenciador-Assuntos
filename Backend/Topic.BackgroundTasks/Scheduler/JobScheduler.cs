using Quartz;
using Topic.BackgroundTasks.Job;

namespace Topic.BackgroundTasks.Scheduler;

internal sealed class JobScheduler : IJobScheduler
{
    private readonly IScheduler _scheduler;

    public JobScheduler(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public async Task AddJob(Guid newsletterId)
    {
        var jobDetail = JobBuilder.Create<SyncNewsJob>()
            .WithIdentity($"news_{newsletterId}")
            .UsingJobData("NewsletterId", newsletterId)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"news_{newsletterId}Trigger")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInHours(1))
            .Build();

        await _scheduler.ScheduleJob(jobDetail, trigger);
    }

    public async Task RemoveJob(Guid newsletterId)
    {
        var jobKey = new JobKey($"news_{newsletterId}");
        await _scheduler.DeleteJob(jobKey);
    }
}
