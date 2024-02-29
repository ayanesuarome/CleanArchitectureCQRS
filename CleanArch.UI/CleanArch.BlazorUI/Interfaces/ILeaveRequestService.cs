using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Interfaces;

public interface ILeaveRequestService
{
    Task<Response<Guid>> CreateLeaveRequestAsync(LeaveRequestVM model);
    Task<AdminLeaveRequestVM> GetAdminLeaveRequestListAsync();
    Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequestListAsync();
    Task<LeaveRequestVM> GetLeaveRequestAsync(int id);
    Task<Response<Guid>> ApprovedLeaveRequestAsync(int id, bool status);
    Task<Response<Guid>> CancelLeaveRequestAsync(int id);
}
