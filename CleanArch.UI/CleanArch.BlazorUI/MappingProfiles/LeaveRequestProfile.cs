using AutoMapper;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestVM, CreateLeaveRequestCommand>();

        CreateMap<LeaveRequestDto, LeaveRequestVM>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(s => s.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(s => s.EndDate))
            .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(s => s.DateRequested));

        CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(s => s.StartDate.DateTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(s => s.EndDate.DateTime))
            .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(s => s.DateRequested));
    }
}
