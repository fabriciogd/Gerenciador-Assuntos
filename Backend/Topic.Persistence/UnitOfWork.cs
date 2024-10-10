using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using Topic.Application.Contracts.Context;
using Topic.Persistence.Contexts;

namespace Topic.Persistence;

/// <summary>
/// The <see cref="UnitOfWork"/> class implements the  <see cref="IUnitOfWork"/> interface and provides a mechanism 
/// to coordinate database operations and domain events within a transaction.
/// It ensures that changes to the database and the publishing of domain events are handled atomically.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{UnitOfWork}"/> for logging information and errors.</param>
/// <param name="_context">The <see cref="MotoDbContext"/> class represents the Entity Framework database context for the application.
internal sealed class UnitOfWork(TopicDbContext _context, ILogger<UnitOfWork> _logger) : IUnitOfWork
{
    /// <summary>
    /// Saves changes to the database within a transaction and publishes domain events after the commit.
    /// This method uses an execution strategy to handle potential transient failures.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            try
            {
                var rowsAffected = await _context.SaveChangesAsync();

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");

                await transaction.RollbackAsync();
            }
        });
    }
}
