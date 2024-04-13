using CleanArch.Domain.Primitives;

namespace CleanArch.Domain.Repositories;

public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity<int>
{
    Task<IReadOnlyCollection<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task CreateAsync(TEntity entity);
    Task<int> CreateListAsync(List<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
