using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.Utilities;
using CleanArch.Domain.Errors;
using Newtonsoft.Json;

namespace CleanArch.Domain.LeaveTypes;

public sealed record DefaultDays
{
    public const int MinValue = 1;
    public const int MaxValue = 100;

    [JsonConstructor]
    private DefaultDays(int value) => Value = value;

    public int Value { get; }

    public static implicit operator int(DefaultDays value) => value.Value;

    public static Result<DefaultDays> Create(int defaultDays) =>
        Result.Create(defaultDays, DomainErrors.DefaultDays.NullOrEmpty)
        .Ensure(d => defaultDays.InclusiveBetween(MinValue, MaxValue), DomainErrors.DefaultDays.NotInRange(MinValue, MaxValue))
        .Map(d => new DefaultDays(defaultDays));
}
