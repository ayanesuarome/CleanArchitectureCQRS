using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Interfaces;

public interface ILeaveTypeService
{
    Task<IReadOnlyCollection<LeaveTypeVM>> GetLeaveTypeList();
    Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
    Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType);
    Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM leaveType);
    Task<Response<Guid>> DeleteLeaveType(int id);
}