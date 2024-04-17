using AutoMapper;
using CleanArch.BlazorUI.Models.LeaveAllocations;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationDetailsDto, LeaveAllocationVM>();
        CreateMap<LeaveAllocationModel, LeaveAllocationVM>();
    }
}
