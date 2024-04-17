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
            CreateLeaveRequestRequest request = _mapper.Map<CreateLeaveRequestRequest>(model);
            await _client.LeaveRequestsPOSTAsync(request);

            return new Response<Guid>();
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<AdminLeaveRequestVM> GetAdminLeaveRequestListAsync()
    {
        LeaveRequestListDto response = await _client.LeaveRequestsGETAsync();
        IReadOnlyCollection<LeaveRequestVM> models = _mapper.Map<IReadOnlyCollection<LeaveRequestVM>>(response.LeaveRequests);


        AdminLeaveRequestVM model = new(models)
        {
            TotalRequests = response.LeaveRequests.Count,
            ApprovedRequests = response.LeaveRequests.Count(request => request.IsApproved is true),
            PendingRequests = response.LeaveRequests.Count(request => request.IsApproved is null),
            RejectedRequests = response.LeaveRequests.Count(request => request.IsApproved is false)
        };

        return model;
    }

    public async Task<EmployeeLeaveRequestVM> GetEmployeeLeaveRequestListAsync()
    {
        LeaveRequestListDto leaveRequestResponse = await _client.LeaveRequestsGET2Async();
        LeaveAllocationListDto allocationResponse = await _client.LeaveAllocationsGET2Async();

        //IReadOnlyCollection<LeaveAllocationVM> leaveAllocations = 
        //    _mapper.Map<IReadOnlyCollection<LeaveAllocationVM>>(allocationResponse.LeaveAllocationList);
        IReadOnlyCollection<LeaveRequestVM> leaveRequests = 
            _mapper.Map<IReadOnlyCollection<LeaveRequestVM>>(leaveRequestResponse.LeaveRequests);

        EmployeeLeaveRequestVM model = new(leaveRequests);

        return model;
    }

    public async Task<LeaveRequestVM> GetLeaveRequestAsync(int id)
    {
        LeaveRequestDetailsDto leaveRequest = await _client.LeaveRequestsGET3Async(id);
        return _mapper.Map<LeaveRequestVM>(leaveRequest);
    }

    public async Task<Response<Guid>> ApprovedLeaveRequestAsync(int id, bool status)
    {
        ChangeLeaveRequestApprovalRequest request = new()
        {
            Approved = status
        };

        try
        {
            await _client.UpdateApprovalAsync(id, request);
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
