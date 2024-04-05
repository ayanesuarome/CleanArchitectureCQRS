using AutoMapper;
using CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;
using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
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

        CreateMap<CreateLeaveType.Command, LeaveType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore());

        CreateMap<UpdateLeaveTypeCommand, LeaveType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore());
    }
}
