using AutoMapper;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;
using CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveRequests;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>()
            .ForMember(dest => dest.Employee, opt => opt.MapFrom<FieldResolverEmployeeForLeaveRequestDto>());

        CreateMap<LeaveRequest, LeaveRequestDetailsDto>()
            .ForMember(dest => dest.DateActioned, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.MapFrom<FieldResolverEmployeeForLeaveRequestDetailsDto>());

        CreateMap<CreateLeaveRequest.Command, LeaveRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
            .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
            .ForMember(dest => dest.IsCancelled, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(s => DateTimeOffset.Now))
            .ForMember(dest => dest.RequestingEmployeeId, opt => opt.MapFrom<FieldResolverUserId>());

        CreateMap<UpdateLeaveRequest.Command, LeaveRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveTypeId, opt => opt.Ignore())
            .ForMember(dest => dest.LeaveType, opt => opt.Ignore())
            .ForMember(dest => dest.IsApproved, opt => opt.Ignore())
            .ForMember(dest => dest.DateRequested, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DateModified, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.RequestingEmployeeId, opt => opt.Ignore());

        CreateMap<CreateLeaveRequestRequest, CreateLeaveRequest.Command>();
        CreateMap<UpdateLeaveRequestRequest, UpdateLeaveRequest.Command>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<ChangeLeaveRequestApprovalRequest, CancelLeaveRequest.Command>();
    }
}
