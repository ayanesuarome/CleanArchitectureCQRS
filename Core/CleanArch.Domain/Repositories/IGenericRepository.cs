using CleanArch.Domain.Primitives;

namespace CleanArch.Domain.Repositories;

public interface IGenericRepository<TEntity>
    where TEntity : Entity<Guid>
{
    Task<IReadOnlyCollection<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task CreateAsync(TEntity entity);
    void Create(TEntity entity);
    Task CreateListAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
