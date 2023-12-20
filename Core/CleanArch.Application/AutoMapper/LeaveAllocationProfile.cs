using AutoMapper;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>();
    }
}
