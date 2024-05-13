﻿using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Entities;

public sealed class LeaveAllocation : Entity<Guid>, IAuditableEntity
{
    public LeaveAllocation(int period, LeaveType leaveType, Guid employeeId)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(period, "The period is required.", nameof(period));
        Ensure.NotNull(leaveType, "The leave type is required.", nameof(leaveType));
        Ensure.NotEmpty(employeeId, "The employee ID is required.", nameof(employeeId));

        Period = period;
        LeaveTypeId = leaveType.Id;
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
    public Guid LeaveTypeId { get; private set; }
    public Guid EmployeeId { get; private set; }

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public Guid CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public Guid? ModifiedBy { get; }

    #endregion

    public Result ValidateHasEnoughDays(DateOnly start, DateOnly end)
    {
        if (end.DayNumber - start.DayNumber > NumberOfDays)
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
