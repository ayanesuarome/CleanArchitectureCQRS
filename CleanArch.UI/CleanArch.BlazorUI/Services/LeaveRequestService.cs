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

    public async Task<Response<AdminLeaveRequestVM>> GetAdminLeaveRequestListAsync()
    {
        try
        {
            ICollection<LeaveRequestDto> leaveRequests = await _client.AdminLeaveRequestAsync();

            AdminLeaveRequestVM model = new()
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(request => request.IsApproved == true),
                PendingRequests = leaveRequests.Count(request => request.IsApproved == null),
                RejectedRequests = leaveRequests.Count(request => request.IsApproved == false),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };

            return new Response<AdminLeaveRequestVM> { Data = model };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<AdminLeaveRequestVM>(ex);
        }
    }
}
