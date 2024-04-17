using CleanArch.BlazorUI.Models.Identity;

namespace CleanArch.BlazorUI.Models.LeaveRequests;

public class EmployeeLeaveRequestVM
{
    public EmployeeLeaveRequestVM() : this([])
    {
    }

    public EmployeeLeaveRequestVM(IReadOnlyCollection<LeaveRequestVM> leaveRequests)
    {
        LeaveRequests = leaveRequests;
    }

    public IReadOnlyCollection<LeaveRequestVM> LeaveRequests { get; }
    public EmployeeVM? Employee => LeaveRequests.FirstOrDefault()?.Employee;
}
