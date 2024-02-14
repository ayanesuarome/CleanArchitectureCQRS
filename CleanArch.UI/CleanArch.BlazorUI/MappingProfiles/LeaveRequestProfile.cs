using AutoMapper;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestVM, CreateLeaveRequestCommand>();

        CreateMap<LeaveRequestDto, LeaveRequestVM>();

        CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>();
    }
}
