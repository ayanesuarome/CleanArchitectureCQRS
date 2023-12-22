using AutoMapper;
using CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>();

        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();

        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore());

        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore());
    }
}
