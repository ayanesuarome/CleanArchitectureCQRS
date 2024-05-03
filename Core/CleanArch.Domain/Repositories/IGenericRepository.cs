using CleanArch.Domain.Primitives;

namespace CleanArch.Domain.Repositories;

public interface IGenericRepository<TEntity>
    where TEntity : Entity<Guid>
{
    Task<IReadOnlyCollection<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task CreateAsync(TEntity entity);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
