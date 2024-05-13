using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ILeaveTypeRepository
{
    Task<LeaveType> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<LeaveType>> GetAsync();
    void Add(LeaveType entity);
    void Update(LeaveType entity);
    void Delete(LeaveType entity);
    Task<bool> IsUniqueAsync(string name, CancellationToken token = default);
    Task<bool> AnyAsync(Guid id, CancellationToken token = default);
}
