using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Entities;

public class LeaveRequest : BaseEntity<int>, ISoftDeletableEntity
{
    public LeaveRequest(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        LeaveType leaveType,
        DateTimeOffset dateRequested,
        string? requestComments,
        string employeeId
        )
    {
        Ensure.NotEmpty(startDate, "The start date is required.", nameof(startDate));
        Ensure.NotEmpty(endDate, "The end date is required.", nameof(endDate));
        Ensure.NotNull(leaveType, "The leave type is required.", nameof(leaveType));
        Ensure.NotNull(employeeId, "The employee ID is required.", nameof(employeeId));

        StartDate = startDate;
        EndDate = endDate;
        LeaveTypeId = leaveType.Id;
        LeaveType = leaveType;
        DateRequested = dateRequested;
        RequestComments = requestComments;
        RequestingEmployeeId = employeeId;
    }

    // <summary>
    /// Initializes a new instance of the class <see cref="LeaveRequest"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private LeaveRequest()
    {
    }

    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset EndDate { get; private set; }
    public int LeaveTypeId { get; private set; }
    public LeaveType? LeaveType { get; set; }
    public DateTimeOffset DateRequested { get; private set; }
    public string? RequestComments { get; private set; }
    public bool? IsApproved { get; private set; }
    public bool IsCancelled { get; private set; }
    public string RequestingEmployeeId { get; private set; }
    public bool IsDeleted { get; }
    public DateTimeOffset? DeletedOn { get; }

    public int DaysRequested() => (int)(EndDate - StartDate).TotalDays + 1;

    public Result Reject()
    {
        if(IsApproved is false)
        {
            return Result.Failure(DomainErrors.LeaveRequest.AlreadyRejected);
        }

        IsApproved = false;

        return Result.Success();
    }
    
    public Result Approve()
    {
        if(IsApproved is true)
        {
            return Result.Failure(DomainErrors.LeaveRequest.AlreadyRejected);
        }

        IsApproved = true;

        return Result.Success();
    }

    public Result Cancel()
    {
        if (IsCancelled)
        {
            return Result.Failure(DomainErrors.LeaveRequest.AlreadyCanceled);
        }

        IsCancelled = true;

        return Result.Success();
    }
}
