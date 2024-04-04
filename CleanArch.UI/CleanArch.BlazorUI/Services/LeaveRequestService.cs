using AutoMapper;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveAllocations;
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

        AdminLeaveRequestVM model = new()
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(request => request.IsApproved is true),
            PendingRequests = leaveRequests.Count(request => request.IsApproved is null),
            RejectedRequests = leaveRequests.Count(request => request.IsApproved is false),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }

    public async Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequestListAsync()
    {
        ICollection<LeaveRequestDto> leaveRequests = await _client.LeaveRequestAllAsync();
        ICollection<LeaveAllocationDto> allocations = await _client.LeaveAllocationAllAsync();

        EmployeeLeaveRequestVM model = new()
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
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
            return new Response<Guid>();
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> CancelLeaveRequestAsync(int id)
    {
        try
        {
            await _client.CancelRequestAsync(id);
            return new Response<Guid>();
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
