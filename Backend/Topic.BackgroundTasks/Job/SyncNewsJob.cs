using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl.AdoJobStore.Common;
using System;
using Topic.Application.Contracts.Context;
using Topic.Application.Contracts.News;
using Topic.Domain.Entities;
using Topic.Domain.Extensions;
using Topic.Domain.Repositories;

namespace Topic.BackgroundTasks.Job;

public class SyncNewsJob(IServiceProvider _serviceProvider) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var newsletterId = context.JobDetail.JobDataMap.GetGuid("NewsletterId");

        using (var scope = _serviceProvider.CreateScope())
        {
            var _repository = scope.ServiceProvider.GetService<INewsletterRepository>();
            var _newsService = scope.ServiceProvider.GetService<INewsService>();
            var _unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var newsletter = await _repository.GetByIdAsync(newsletterId, context.CancellationToken);

            if (newsletter == null)
                return;

            var response = await _newsService.SearchAsync(newsletter.Keywords, newsletter.LinksCount, context.CancellationToken);

            foreach (var article in response.Articles)
                newsletter.AddLink(NewsletterLink.Create(article?.Title?.Truncate(100) ?? "", article?.Description?.Truncate(500) ?? "", article.Url));

            _repository.Update(newsletter);

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}