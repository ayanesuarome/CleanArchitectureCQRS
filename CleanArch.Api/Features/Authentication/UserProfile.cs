using AutoMapper;
using CleanArch.Api.Features.Users.CreateUsers;
using CleanArch.Contracts.Identity;

namespace CleanArch.Api.Features.Authentication;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistrationRequest, CreateUser.Command>();
        //.ForCtorParam(nameof(ChangeLeaveRequestApproval.Command.Id), opt => opt.MapFrom(src => src.Approved))
    }
}
