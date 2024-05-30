using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Domain.Authentication;

public record RolePermissionId(Guid Id) : IEntityKey;
