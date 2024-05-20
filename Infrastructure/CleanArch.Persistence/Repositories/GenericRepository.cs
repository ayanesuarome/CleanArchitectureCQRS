using CleanArch.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Persistence.Repositories;

/// <summary>
/// Represents the generic repository with the most common repository methods.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
internal abstract class GenericRepository<TEntity, TEntityKey>
    where TEntity : Entity<TEntityKey>
    where TEntityKey : class, IEquatable<TEntityKey>
{
    protected GenericRepository(CleanArchEFDbContext dbContext) => DbContext = dbContext;

    /// <summary>
    /// Gets the database context.
    /// </summary>
    protected CleanArchEFDbContext DbContext { get; }

    private DbSet<TEntity> Entities => DbContext.Set<TEntity>();

    protected virtual IQueryable<TEntity> Table => Entities;
    protected virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    /// <summary>
    /// Gets the entity with the specified identifier.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <returns>The entity with the specified identifier.</returns>
    public async Task<TEntity> GetByIdAsync(TEntityKey id) => await Table.FirstOrDefaultAsync(e => e.Id.Equals(id));
    public async Task<IReadOnlyCollection<TEntity>> GetAsync() => await TableNoTracking.ToArrayAsync();

    public async Task<TEntity> GetAsNoTrackingByIdAsync(TEntityKey id) => await TableNoTracking.FirstOrDefaultAsync(e => e.Id.Equals(id));

    public async Task CreateAsync(TEntity entity)
    {
        await DbContext.AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Inserts the specified entity into the database.
    /// </summary>
    /// <param name="entity">The entity to be inserted into the database.</param>
    public void Add(TEntity entity) => DbContext.Add(entity);

    /// <summary>
    /// Inserts the specified entities to the database.
    /// </summary>
    /// <param name="entities">The entities to be inserted into the database.</param>
    public void AddRange(IReadOnlyCollection<TEntity> entities) => DbContext.AddRange(entities);

    /// <summary>
    /// Updates the specified entity in the database.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    public void Update(TEntity entity) => DbContext.Update(entity);

    /// <summary>
    /// Removes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to be removed from the database.</param>
    public void Delete(TEntity entity) => DbContext.Remove(entity);
}
