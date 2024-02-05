using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper.LeaveRequests;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>()
            .ForMember(dest => dest.Employee, opt => opt.MapFrom<FieldResolverEmployee>());

        CreateMap<LeaveRequest, LeaveRequestDetailsDto>()
            .ForMember(dest => dest.DateActioned, opt => opt.Ignore());

        CreateMap<CreateLeaveRequestCommand, LeaveRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
            .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
            .ForMember(dest => dest.IsCancelled, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(s => DateTimeOffset.Now))
            .ForMember(dest => dest.RequestingEmployeeId, opt => opt.MapFrom<FieldResolverUserId>());

        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveTypeId, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
            .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
            .ForMember(dest => dest.DateRequested, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ForMember(dest => dest.RequestingEmployeeId, opt => opt.Ignore());
    }
}
