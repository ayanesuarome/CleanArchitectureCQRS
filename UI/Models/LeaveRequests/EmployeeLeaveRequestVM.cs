namespace CleanArch.BlazorUI.Models.LeaveRequests;

internal sealed class EmployeeLeaveRequestVM
{
    public EmployeeLeaveRequestVM() : this([])
    {
    }

    public EmployeeLeaveRequestVM(IReadOnlyCollection<LeaveRequestVM> leaveRequests)
    {
        LeaveRequests = leaveRequests;
    }

    public IReadOnlyCollection<LeaveRequestVM> LeaveRequests { get; }
    public string? EmployeeFullName => LeaveRequests.FirstOrDefault()?.EmployeeFullName;
}
