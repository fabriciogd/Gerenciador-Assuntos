using Topic.Domain.Abstractions;
using Topic.Domain.Entities;

namespace Topic.Domain.Repositories;

public interface INewsletterRepository : IRepository<Newsletter>
{
    Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken);
}
