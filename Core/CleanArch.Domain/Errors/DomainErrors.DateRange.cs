using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class DateRange
    {
        public static Error RangeIsNotValid => new ("DateRange.RangeIsNotValid", "The range is not valid.");
    }
}
