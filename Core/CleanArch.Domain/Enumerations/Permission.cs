namespace CleanArch.Domain.Enumerations;

public enum Permission
{
    AccessLeaveTypes = 1,
    CreateLeaveType = 2,
    UpdateLeaveType = 3,
    DeleteLeaveType = 4,
    
    AccessLeaveRequests = 5,
    CreateLeaveRequest = 6,
    UpdateLeaveRequest = 7,
    DeleteLeaveRequest = 8,
    CancelLeaveRequest = 9,
    ChangeLeaveRequestApproval = 10,

    AccessLeaveAllocations = 11,
    CreateLeaveAllocation = 12,
    UpdateLeaveAllocation = 13,
    DeleteLeaveAllocation = 14
}
