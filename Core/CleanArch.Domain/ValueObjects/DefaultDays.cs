using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Utilities;

namespace CleanArch.Domain.ValueObjects;

public sealed record DefaultDays
{
    public const int MinValue = 1;
    public const int MaxValue = 100;

    private DefaultDays(int value) => Value = value;
    
    public int Value { get; }

    public static implicit operator int(DefaultDays value) => value.Value;

    public static Result<DefaultDays> Create(int defaultDays) =>
        Result.Create(defaultDays, DomainErrors.DefaultDays.NullOrEmpty)
        .Ensure(d => defaultDays.InclusiveBetween(MinValue, MaxValue), DomainErrors.DefaultDays.NotInRange(MinValue, MaxValue))
        .Map(d => new DefaultDays(defaultDays));
}
