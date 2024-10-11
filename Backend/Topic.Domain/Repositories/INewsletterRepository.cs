using Topic.Domain.Abstractions;
using Topic.Domain.Entities;
using Topic.Domain.Enums;

namespace Topic.Domain.Repositories;

public interface INewsletterRepository : IRepository<Newsletter>
{
    Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken);

    Task<List<NewsletterLink>> GetAllLinks(Guid id, CancellationToken cancellationToken);

    Task<Dictionary<StatusEnum, int>> GetGroups(CancellationToken cancellationToken);
}
