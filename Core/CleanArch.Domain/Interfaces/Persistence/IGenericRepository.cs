using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces.Persistence;

public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}
