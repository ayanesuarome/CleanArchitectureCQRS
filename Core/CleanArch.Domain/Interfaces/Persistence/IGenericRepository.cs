﻿using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces.Persistence;

public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task CreateAsync(TEntity entity);
    Task CreateListAsync(List<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
