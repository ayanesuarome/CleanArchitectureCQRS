using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Persistence.Repositories;

internal sealed class LeaveRequestRepository : GenericRepository<LeaveRequest, LeaveRequestId>, ILeaveRequestRepository
{
    public LeaveRequestRepository(CleanArchEFWriteDbContext dbContext)
        : base(dbContext)
    {
    }
}
