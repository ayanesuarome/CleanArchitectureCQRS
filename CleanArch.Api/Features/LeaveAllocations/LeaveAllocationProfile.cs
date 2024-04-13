﻿using AutoMapper;
using CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;
using CleanArch.Contracts.LeaveAllocations;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveAllocations;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<UpdateLeaveAllocation.Command, LeaveAllocation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

        CreateMap<UpdateLeaveAllocationRequest, UpdateLeaveAllocation.Command>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<LeaveAllocation, LeaveAllocationListDto.LeaveAllocationModel>();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
    }
}
