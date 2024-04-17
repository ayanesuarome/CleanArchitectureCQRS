using AutoMapper;
using CleanArch.Api.Features.Authentication.CreateUsers;
using CleanArch.Contracts.Identity;

namespace CleanArch.Api.Features.Authentication;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<RegistrationRequest, CreateUser.Command>();
        CreateMap<LoginRequest, Login.Login.Command>();
    }
}
