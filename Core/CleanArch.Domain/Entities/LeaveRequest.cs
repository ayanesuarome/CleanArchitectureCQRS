using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Utilities;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Domain.Entities;

public sealed class LeaveRequest : BaseEntity<int>, IAuditableEntity, ISoftDeletableEntity
{
    public LeaveRequest(
        DateRange range,
        LeaveType leaveType,
        Comment? comments,
        string employeeId)
    {
        Ensure.NotNull(range, "The date range is required.", nameof(range));
        Ensure.NotNull(leaveType, "The leave type is required.", nameof(leaveType));
        Ensure.NotNull(employeeId, "The employee ID is required.", nameof(employeeId));

        Range = range;
        LeaveTypeId = leaveType.Id;
        LeaveTypeName = leaveType.Name;
        Comments = comments;
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

    public DateRange Range { get; private set; }
    public int LeaveTypeId { get; private set; }
    public Name LeaveTypeName { get; private set; }
    public Comment? Comments { get; private set; }
    public bool? IsApproved { get; private set; }
    public bool IsCancelled { get; private set; }
    public string RequestingEmployeeId { get; private set; }


    #region Soft Deletable

    public bool IsDeleted { get; }
    public DateTimeOffset? DeletedOn { get; }

    #endregion

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public string CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public string? ModifiedBy { get; }

    #endregion

    public int DaysRequested() => (Range.EndDate.DayNumber - Range.StartDate.DayNumber) + 1;

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

    public void UpdateDateRange(DateRange range)
    {
        if(range == Range)
        {
            return;
        }

        Range = range;
    }

    public void UpdateComments(Comment comments)
    {
        if(comments == Comments)
        {
            return;
        }

        Comments = comments;
    }
}
