using CleanArch.Domain.Core.ValueObjects;

namespace CleanArch.Domain.LeaveTypes;

public interface ILeaveTypeRepository
{
    Task<LeaveType> GetByIdAsync(LeaveTypeId id);
    Task<IReadOnlyCollection<LeaveType>> GetAsync();
    void Add(LeaveType entity);
    void Update(LeaveType entity);
    void Delete(LeaveType entity);
    Task<bool> IsUniqueAsync(Name name, CancellationToken token = default);
    Task<bool> AnyAsync(LeaveTypeId id, CancellationToken token = default);
}
