using CleanArch.Domain.Primitives;
using CleanArch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

public class GenericRepository<TEntity>(CleanArchEFDbContext dbContext) : IGenericRepository<TEntity>
    where TEntity : BaseEntity<int>
{
    protected readonly CleanArchEFDbContext _dbContext = dbContext;
    protected virtual DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public async Task<IReadOnlyCollection<TEntity>> GetAsync()
    {
        return await TableNoTracking.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await TableNoTracking.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task CreateAsync(TEntity entity)
    {
        try
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<int> CreateListAsync(List<TEntity> entities)
    {
        try
        {
            await _dbContext.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        try
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        try
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
