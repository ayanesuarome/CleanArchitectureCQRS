namespace CleanArch.Application.Abstractions.Authentication;

public interface IPermissionService
{
    // Gets the permissions for users.
    Task<HashSet<string>> GetPermissionsAsync(Guid memberId);
}
