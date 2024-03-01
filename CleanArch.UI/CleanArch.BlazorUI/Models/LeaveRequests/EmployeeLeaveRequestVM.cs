using CleanArch.BlazorUI.Models.Identity;
using CleanArch.BlazorUI.Models.LeaveAllocations;

namespace CleanArch.BlazorUI.Models.LeaveRequests;

public class EmployeeLeaveRequestVM
{
    public List<LeaveAllocationVM> LeaveAllocations { get; set; } = [];
    public List<LeaveRequestVM> LeaveRequests { get; set; } = [];
    public EmployeeVM? Employee => LeaveRequests.FirstOrDefault()?.Employee;
}
