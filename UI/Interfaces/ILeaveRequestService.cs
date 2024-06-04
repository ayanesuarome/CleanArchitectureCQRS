using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Interfaces;

internal interface ILeaveRequestService
{
    Task<Response<Guid>> CreateLeaveRequestAsync(LeaveRequestVM model);
    Task<AdminLeaveRequestVM> GetAdminLeaveRequestListAsync();
    Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequestListAsync();
    Task<LeaveRequestVM> GetLeaveRequestAsync(Guid id);
    Task<Response<Guid>> ApprovedLeaveRequestAsync(Guid id, bool status);
    Task<Response<Guid>> CancelLeaveRequestAsync(Guid id);
}
