using CleanArch.Domain.Primitives;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Entities;

public sealed class LeaveType : BaseEntity<int>, IAuditableEntity
{
    public LeaveType(string name, int defaultDays)
    {
        Ensure.NotNull(name, "The name is required", nameof(name));
        Ensure.NotEmpty(defaultDays, "The default days is required", nameof(defaultDays));

        Name = name;
        DefaultDays = defaultDays;
    }

    // <summary>
    /// Initializes a new instance of the class <see cref="LeaveType"/>.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private LeaveType()
    {
    }

    public string Name { get; private set; }
    public int DefaultDays { get; private set; }

    #region Auditable

    public DateTimeOffset DateCreated { get; }
    public string CreatedBy { get; }
    public DateTimeOffset? DateModified { get; }
    public string? ModifiedBy { get; }

    #endregion
}
