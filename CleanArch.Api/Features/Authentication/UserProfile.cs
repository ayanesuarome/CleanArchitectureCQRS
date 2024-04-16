using AutoMapper;
using CleanArch.Api.Features.Authentication.CreateUsers;
using CleanArch.Contracts.Identity;

namespace CleanArch.Api.Features.Authentication;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<RegistrationRequest, CreateUser.Command>();
        //.ForCtorParam(nameof(ChangeLeaveRequestApproval.Command.Id), opt => opt.MapFrom(src => src.Approved))

        CreateMap<LoginRequest, Login.Login.Command>();
    }
}
