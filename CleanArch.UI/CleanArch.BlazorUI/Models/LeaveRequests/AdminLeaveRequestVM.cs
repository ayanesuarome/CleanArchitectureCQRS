namespace CleanArch.BlazorUI.Models.LeaveRequests;

public class AdminLeaveRequestVM
{
    public AdminLeaveRequestVM() : this([])
    {
    }
    public AdminLeaveRequestVM(IReadOnlyCollection<LeaveRequestVM> leaveRequests) => LeaveRequests = leaveRequests;

    public int TotalRequests { get; set; }
    public int ApprovedRequests { get; set; }
    public int PendingRequests { get; set; }
    public int RejectedRequests { get; set; }
    public IReadOnlyCollection<LeaveRequestVM> LeaveRequests { get; }
}
