using AutoMapper;
using CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;
using CleanArch.Application.Features.LeaveTypesDetails.Queries.GetLeaveTypesDetails;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>()
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<LeaveType, LeaveTypeDetailsDto>();
    }
}
