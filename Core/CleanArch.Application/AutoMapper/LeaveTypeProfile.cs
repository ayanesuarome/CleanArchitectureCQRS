using AutoMapper;
using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveTypes;
using CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveTypes;
using CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;
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

        CreateMap<CreateLeaveTypeCommand, LeaveType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore());

        CreateMap<UpdateLeaveTypeCommand, LeaveType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore());
    }
}
