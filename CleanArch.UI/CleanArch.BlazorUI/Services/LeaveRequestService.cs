using AutoMapper;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveRequestService(IClient client, IMapper mapper) : BaseHttpService(client), ILeaveRequestService
{
    private readonly IMapper _mapper = mapper;

    public async Task<Response<Guid>> CreateLeaveRequestAsync(LeaveRequestVM model)
    {
        try
        {
            CreateLeaveRequestCommand command = _mapper.Map<CreateLeaveRequestCommand>(model);
            await _client.LeaveRequestPOSTAsync(command);

            return new Response<Guid>();
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<AdminLeaveRequestVM> GetAdminLeaveRequestListAsync()
    {
        ICollection<LeaveRequestDto> leaveRequests = await _client.AdminLeaveRequestAsync();

        return new()
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(request => request.IsApproved == true),
            PendingRequests = leaveRequests.Count(request => request.IsApproved == null),
            RejectedRequests = leaveRequests.Count(request => request.IsApproved == false),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };
    }

    public async Task<LeaveRequestVM> GetLeaveRequestAsync(int id)
    {
        LeaveRequestDetailsDto leaveRequest = await _client.LeaveRequestGETAsync(id);
        return _mapper.Map<LeaveRequestVM>(leaveRequest);
    }

    public async Task<Response<Guid>> ApprovedLeaveRequestAsync(int id, bool status)
    {
        ChangeLeaveRequestApprovalCommand command = new()
        {
            Approved = status
        };

        try
        {
            await _client.UpdateApprovalAsync(id, command);
            return new Response<Guid>() { Success = true };
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
