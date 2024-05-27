using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Interfaces;

public interface ILeaveAllocationService
{
    Task<Response<Guid>> CreateLeaveAllocations(Guid leaveTypeId);
}
