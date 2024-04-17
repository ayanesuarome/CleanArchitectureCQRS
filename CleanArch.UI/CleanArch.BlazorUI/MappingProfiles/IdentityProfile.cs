using AutoMapper;
using CleanArch.BlazorUI.Models.Identity;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.MappingProfiles;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<RegistrationRequestVM, RegistrationRequest>();
        CreateMap<LoginVM, LoginRequest>();
        CreateMap<EmployeeVM, Employee>()
            .ReverseMap();
    }
}
