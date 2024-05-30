using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.Authentication;

public record PermissionId(int Id) : IEntityKey;
