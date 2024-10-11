using Microsoft.EntityFrameworkCore;
using Topic.Domain.Entities;
using Topic.Domain.Enums;
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

    public async Task<List<NewsletterLink>> GetAllLinks(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(a => a.Links)
            .AsNoTracking()
            .Where(entity => entity.Id == id)
            .SelectMany(entity => entity.Links)
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<StatusEnum, int>> GetGroups(CancellationToken cancellationToken)
    {
        return await _dbSet
            .GroupBy(a => a.Status)
            .ToDictionaryAsync(a => a.Key, a => a.Count());
    }
}
