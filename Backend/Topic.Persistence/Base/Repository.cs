﻿using Microsoft.EntityFrameworkCore;
using Topic.Domain.Abstractions;
using Topic.Domain.Base;
using Topic.Persistence.Contexts;

namespace Topic.Persistence.Base;

/// <summary>
/// A generic repository that provides common database operations for entities.
/// </summary>
internal class Repository<TEntity>(TopicDbContext _context) : IRepository<TEntity>
    where TEntity : AggregateRoot
{
    protected readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _dbSet.AddAsync(entity, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves an entity by its primary key ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _dbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);

    /// <summary>
    /// Removes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    /// <summary>
    /// Updates the specified entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(TEntity entity) => _dbSet.Update(entity);

    /// <summary>
    /// Asynchronously retrieves a list of entities that match a given condition.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation if needed.</param>
    /// <returns>A list of entities that match the condition.</returns>
    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => _dbSet.AsNoTracking().ToListAsync(cancellationToken);
}