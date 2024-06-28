using CleanArch.Domain.Core.Primitives;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.Utilities;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes.Events;

namespace CleanArch.Domain.LeaveTypes;

public sealed class LeaveType : AggregateRoot<LeaveTypeId>, IAuditableEntity
{
    private LeaveType(Name name, DefaultDays defaultDays)
        : base(new LeaveTypeId(Guid.NewGuid()))
    {
        Ensure.NotNull(name, "The name is required");
        Ensure.NotNull(defaultDays, "The default days is required");

        Name = name;
        DefaultDays = defaultDays;
    }

    /// <summary>
    /// Initializes a new instance of the class <see cref="LeaveType"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private LeaveType()
    {
    }

    public Name Name { get; private set; }
    public DefaultDays DefaultDays { get; private set; }

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public Guid CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public Guid? ModifiedBy { get; }

    #endregion

    public static Result<LeaveType> Create(
        Name name,
        DefaultDays defaultDays,
        LeaveTypeNameUniqueRequirement nameUniqueRequirement)
    {
        if (!nameUniqueRequirement.IsSatified)
        {
            return Result.Failure<LeaveType>(Errors.DomainErrors.LeaveType.DuplicateName);
        }

        LeaveType leaveType = new(name, defaultDays);

        leaveType.RaiseDomainEvent(new LeaveTypeCreatedDomainEvent(leaveType.Id));

        return Result.Success(leaveType);
    }

    public Result UpdateName(Name name, LeaveTypeNameUniqueRequirement nameUniqueRequirement)
    {
        if (name == Name)
        {
            return Result.Success();
        }

        if (!nameUniqueRequirement.IsSatified)
        {
            return Result.Failure(Errors.DomainErrors.LeaveType.DuplicateName);
        }

        Name = name;
        return Result.Success();
    }

    public void UpdateDefaultDays(DefaultDays defaultDays)
    {
        if (defaultDays == DefaultDays)
        {
            return;
        }

        DefaultDays = defaultDays;
    }

    public void NotifyDeletion()
    {
        RaiseDomainEvent(new LeaveTypeDeletedDomainEvent(Id));
    }

    public void NotifyUpdate()
    {
        RaiseDomainEvent(new LeaveTypeUpdatedDomainEvent(Id));
    }
}
