using AutoMapper;
using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveTypes;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        //CreateMap<LeaveTypeDto, LeaveType>()
        //    .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
        //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
        //    .ForMember(dest => dest.DateModified, opt => opt.Ignore())
        //    .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
        //    .ReverseMap();

        //CreateMap<LeaveType, LeaveTypeDetailsDto>();

        CreateMap<CreateLeaveTypes.CreateLeaveType.Command, LeaveType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

        CreateMap<CreateLeaveTypeRequest, CreateLeaveTypes.CreateLeaveType.Command>();
    }
}
