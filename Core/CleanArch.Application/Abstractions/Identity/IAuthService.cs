using CleanArch.Contracts.Identity;

namespace CleanArch.Application.Abstractions.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}
