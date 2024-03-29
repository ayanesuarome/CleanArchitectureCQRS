﻿using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken token = default);
    Task<bool> AnyAsync(int id, CancellationToken token = default);
}
