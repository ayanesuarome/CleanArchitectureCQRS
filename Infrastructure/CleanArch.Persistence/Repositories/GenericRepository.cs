using CleanArch.Domain.Primitives;
using CleanArch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

public class GenericRepository<TEntity>(CleanArchEFDbContext dbContext) : IGenericRepository<TEntity>
    where TEntity : Entity<Guid>
{
    protected readonly CleanArchEFDbContext _dbContext = dbContext;
    protected virtual DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public async Task<IReadOnlyCollection<TEntity>> GetAsync() => await TableNoTracking.ToArrayAsync();

    public async Task<TEntity> GetByIdAsync(Guid id) => await TableNoTracking.FirstOrDefaultAsync(e => e.Id == id);

    public async Task CreateAsync(TEntity entity) => await _dbContext.AddAsync(entity);

    public async Task CreateListAsync(IEnumerable<TEntity> entities) => _dbContext.AddRangeAsync(entities);

    public async Task UpdateAsync(TEntity entity) => _dbContext.Entry(entity).State = EntityState.Modified;

    public async Task DeleteAsync(TEntity entity) => _dbContext.Remove(entity);
}
