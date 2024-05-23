using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Utilities;

namespace CleanArch.Domain.Authentication;

public sealed class Permissions : Enumeration<Permissions>
{
    public Permissions(int id, string name, string description)
        : base(id, name)
    {
        Ensure.NotEmpty(id, "The identifier is required.");
        Ensure.NotNull(name, "The name is required.");
        Ensure.NotNull(description, "The description is required.");

        Description = description;
    }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; private init; }

    #region Leave Types
    public static readonly Permissions AccessLeaveTypes = new(1, nameof(AccessLeaveTypes), "Can get access to leave types");
    public static readonly Permissions CreateLeaveType = new(2, nameof(CreateLeaveType), "Can create leave types");
    public static readonly Permissions UpdateLeaveType = new(3, nameof(UpdateLeaveType), "Can update access to leave types");
    public static readonly Permissions DeleteLeaveType = new(4, nameof(DeleteLeaveType), "Can delete leave types");
    #endregion

    #region Leave Requests
    public static readonly Permissions AccessLeaveRequests = new(5, nameof(AccessLeaveRequests), "Can get access to leave requests");
    public static readonly Permissions CreateLeaveRequest = new(6, nameof(CreateLeaveRequest), "Can create leave requests");
    public static readonly Permissions UpdateLeaveRequest = new(7, nameof(UpdateLeaveRequest), "Can update access to leave requests");
    public static readonly Permissions DeleteLeaveRequest = new(8, nameof(DeleteLeaveRequest), "Can delete leave requests");
    public static readonly Permissions CancelLeaveRequest = new(9, nameof(CancelLeaveRequest), "Can cancel leave requests");
    public static readonly Permissions ChangeLeaveRequestApproval = new(10, nameof(ChangeLeaveRequestApproval), "Can change leave requests approvals");
    #endregion

    #region Leave Allocations
    public static readonly Permissions AccessLeaveAllocations = new(11, nameof(AccessLeaveAllocations), "Can get access to leave allocations");
    public static readonly Permissions CreateLeaveAllocation = new(12, nameof(CreateLeaveAllocation), "Can create leave allocations");
    public static readonly Permissions UpdateLeaveAllocation = new(13, nameof(UpdateLeaveAllocation), "Can update access to leave allocations");
    public static readonly Permissions DeleteLeaveAllocation = new(14, nameof(DeleteLeaveAllocation), "Can delete leave allocations");
    #endregion
}
