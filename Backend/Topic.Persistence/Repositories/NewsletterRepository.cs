using Microsoft.EntityFrameworkCore;
using Topic.Domain.Entities;
using Topic.Domain.Repositories;
using Topic.Persistence.Base;
using Topic.Persistence.Contexts;

namespace Topic.Persistence.Repositories;

internal sealed class NewsletterRepository(TopicDbContext _context) : Repository<Newsletter>(_context), INewsletterRepository
{
    public async Task<bool> ExistsByTitleAsync(string title, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(entity => entity.Title.ToLower() == title.ToLower())
            .AnyAsync(cancellationToken);
    }
}
