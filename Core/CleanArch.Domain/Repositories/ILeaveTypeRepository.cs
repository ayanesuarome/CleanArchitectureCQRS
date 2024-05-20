using CleanArch.Domain.Entities;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Domain.Repositories;

public interface ILeaveTypeRepository
{
    Task<LeaveType> GetByIdAsync(LeaveTypeId id);
    Task<IReadOnlyCollection<LeaveType>> GetAsync();
    void Add(LeaveType entity);
    void Update(LeaveType entity);
    void Delete(LeaveType entity);
    Task<bool> IsUniqueAsync(string name, CancellationToken token = default);
    Task<bool> AnyAsync(LeaveTypeId id, CancellationToken token = default);
}
