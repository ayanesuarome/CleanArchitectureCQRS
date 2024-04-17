using AutoMapper;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDetailDto, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveTypeModel, LeaveTypeVM>();
        CreateMap<LeaveTypeVM, CreateLeaveTypeRequest>();
        CreateMap<LeaveTypeVM, UpdateLeaveTypeRequest>();
    }
}
