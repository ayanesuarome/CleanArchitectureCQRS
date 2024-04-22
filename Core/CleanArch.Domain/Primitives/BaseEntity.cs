using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.Primitives;

public abstract class BaseEntity<T>
{
    protected BaseEntity(T id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected BaseEntity()
    {
    }

    public T Id { get; set; }
}
