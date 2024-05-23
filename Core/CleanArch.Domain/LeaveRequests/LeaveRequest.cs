using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.Utilities;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveRequests.Events;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Domain.LeaveRequests;

public sealed class LeaveRequest : AggregateRoot<LeaveRequestId>, IAuditableEntity, ISoftDeletableEntity
{
    private LeaveRequest(DateRange range, LeaveType leaveType, Guid employeeId, Comment? comments)
        : base(new LeaveRequestId(Guid.NewGuid()))
    {
        Ensure.NotNull(range, "The date range is required.", nameof(range));
        Ensure.NotNull(leaveType, "The leave type is required.", nameof(leaveType));
        Ensure.NotEmpty(employeeId, "The employee ID is required.", nameof(employeeId));

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
    public LeaveTypeId LeaveTypeId { get; private set; }
    public Name LeaveTypeName { get; private set; }
    public Comment? Comments { get; private set; }
    public bool? IsApproved { get; private set; }
    public bool IsCancelled { get; private set; }
    public Guid RequestingEmployeeId { get; private set; }


    #region Soft Deletable

    public bool IsDeleted { get; }
    public DateTimeOffset? DeletedOn { get; }

    #endregion

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public Guid CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public Guid? ModifiedBy { get; }

    #endregion

    public int DaysRequested => Range.EndDate.DayNumber - Range.StartDate.DayNumber + 1;

    public static LeaveRequest Create(
        DateRange range,
        LeaveType leaveType,
        Guid employeeId,
        Comment? comments)
    {
        LeaveRequest request = new(range, leaveType, employeeId, comments);

        request.RaiseDomainEvent(new LeaveRequestCreatedDomainEvent(request));

        return request;
    }

    public Result Reject()
    {
        if (IsApproved is false)
        {
            return Result.Failure(DomainErrors.LeaveRequest.AlreadyRejected);
        }

        IsApproved = false;

        return Result.Success();
    }

    public Result Approve()
    {
        if (IsApproved is true)
        {
            return Result.Failure(DomainErrors.LeaveRequest.AlreadyApproved);
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
        if (range == Range)
        {
            return;
        }

        Range = range;
    }

    public void UpdateComments(Comment? comments)
    {
        if (comments == Comments)
        {
            return;
        }

        Comments = comments;
    }
}
