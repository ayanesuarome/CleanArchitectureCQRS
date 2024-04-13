using AutoMapper;
using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Contracts.LeaveTypes;
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

        CreateMap<LeaveType, LeaveTypeListDto.LeaveTypeModel>();
        CreateMap<LeaveType, LeaveTypeDetailDto>();

        //CreateMap<CreateLeaveTypes.CreateLeaveType.Command, LeaveType>()
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())
        //    .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
        //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
        //    .ForMember(dest => dest.DateModified, opt => opt.Ignore())
        //    .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

        //CreateMap<CreateLeaveTypeRequest, CreateLeaveTypes.CreateLeaveType.Command>();

        CreateMap<UpdateLeaveType.Command, LeaveType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

        CreateMap<UpdateLeaveTypeRequest, UpdateLeaveType.Command>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
