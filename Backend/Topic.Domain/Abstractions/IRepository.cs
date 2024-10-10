using Topic.Domain.Base;

namespace Topic.Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : AggregateRoot
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}
