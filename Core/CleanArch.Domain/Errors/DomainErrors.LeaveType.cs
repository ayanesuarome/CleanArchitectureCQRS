using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class LeaveType
    {
        public static Error NotFound(int id) => new Error($"LeaveType.NotFound", $"The leave type with Id '{id}' was not found.");
        public static Error DuplicateName => new Error("LeaveType.DuplicateName", "The Leave type is already in use.");
    }
}
