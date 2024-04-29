using AutoMapper;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestVM, CreateLeaveRequestRequest>();
        CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>();
    }
}