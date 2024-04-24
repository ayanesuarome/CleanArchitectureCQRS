using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken token = default);
    Task<bool> AnyAsync(Guid id, CancellationToken token = default);
}
