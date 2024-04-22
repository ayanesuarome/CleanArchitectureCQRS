using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Entities;

public sealed class LeaveAllocation : BaseEntity<int>, IAuditableEntity
{
    public LeaveAllocation(int period, LeaveType leaveType, string employeeId)
    {
        Ensure.NotEmpty(period, "The period is required.", nameof(period));
        Ensure.NotNull(leaveType, "The leave type is required.", nameof(leaveType));
        Ensure.NotNull(employeeId, "The employee ID is required.", nameof(employeeId));

        Period = period;
        LeaveTypeId = leaveType.Id;
        LeaveType = leaveType;
        EmployeeId = employeeId;
    }

    // <summary>
    /// Initializes a new instance of the class <see cref="LeaveAllocation"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private LeaveAllocation()
    {
    }

    public int NumberOfDays { get; private set; }
    public int Period { get; private set; }
    public int LeaveTypeId { get; private set; }
    public LeaveType? LeaveType { get; private set; }
    public string EmployeeId { get; private set; }

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public string CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public string? ModifiedBy { get; }

    #endregion

    public Result ValidateHasEnoughDays(DateTimeOffset start, DateTimeOffset end)
    {
        if ((int)(end - start).TotalDays > NumberOfDays)
        {
            return Result.Failure(DomainErrors.LeaveRequest.NotEnoughDays);
        }

        return Result.Success();
    }

    public Result ChangeNumberOfDays(int numberOfDays)
    {
        if(numberOfDays < 0)
        {
            return Result.Failure(DomainErrors.LeaveAllocation.InvalidNumberOfDays(numberOfDays));
        }

        NumberOfDays = numberOfDays;

        return Result.Success();
    }
}
