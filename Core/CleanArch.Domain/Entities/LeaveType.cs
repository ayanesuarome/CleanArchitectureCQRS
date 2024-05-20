using CleanArch.Domain.Primitives;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.Utilities;
using CleanArch.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace CleanArch.Domain.Entities;

public sealed class LeaveType : Entity<Guid>, IAuditableEntity
{
    public LeaveType(Name name, DefaultDays defaultDays)
        : base(Guid.NewGuid())
    {
        Ensure.NotNull(name, "The name is required", nameof(name));
        Ensure.NotNull(defaultDays, "The default days is required", nameof(defaultDays));

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

    public LEaveType
    public Name Name { get; private set; }
    public DefaultDays DefaultDays { get; private set; }

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public Guid CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public Guid? ModifiedBy { get; }

    #endregion

    public async Task<Result> UpdateName(Name name, ILeaveTypeRepository repository)
    {
        if(name == Name)
        {
            return Result.Success();
        }

        if (!await repository.IsUniqueAsync(name.Value))
        {
            return Result.Failure<Unit>(Errors.DomainErrors.LeaveType.DuplicateName);
        }

        Name = name;
        return Result.Success();
    }

    public void UpdateDefaultDays(DefaultDays defaultDays)
    {
        if(defaultDays == DefaultDays)
        {
            return;
        }

        DefaultDays = defaultDays;
    }
}
