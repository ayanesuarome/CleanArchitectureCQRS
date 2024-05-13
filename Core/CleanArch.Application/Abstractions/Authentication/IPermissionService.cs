namespace CleanArch.Application.Abstractions.Authentication;

// Singleton service.
public interface IPermissionService
{
    // Gets the permissions for users.
    Task<HashSet<string>> GetPermissionsAsync(Guid userId);
}
