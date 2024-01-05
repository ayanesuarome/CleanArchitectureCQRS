﻿using CleanArch.Application.Models.Identity;

namespace CleanArch.Application.Interfaces.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}
