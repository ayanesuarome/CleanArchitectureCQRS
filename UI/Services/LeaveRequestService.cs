using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

internal sealed class LeaveRequestService(IClient client) : BaseHttpService(client), ILeaveRequestService
{
    public async Task<Response<Guid>> CreateLeaveRequestAsync(LeaveRequestVM model)
    {
        try
        {
            CreateLeaveRequestRequest request = new()
            {
                LeaveTypeId = model.LeaveTypeId,
                Comments = model.RequestComments,
                StartDate = model.StartDate.ToString(),
                EndDate = model.EndDate.ToString()
            };

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
        LeaveRequestVM[] models = response.LeaveRequests.Select(l =>
        new LeaveRequestVM()
        {
            Id = l.Id,
            StartDate = System.DateOnly.Parse(l.StartDate.ToString()),
            EndDate = System.DateOnly.Parse(l.EndDate.ToString()),
            LeaveTypeId = l.LeaveTypeId,
            LeaveTypeName = l.LeaveTypeName,
            EmployeeFullName = l.EmployeeFullName,
            DateCreated = l.DateCreated,
            RequestComments = l.RequestComments,
            IsApproved = l.IsApproved,
            IsCancelled = l.IsCancelled
        }).ToArray();

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

        LeaveRequestVM[] leaveRequests = leaveRequestResponse.LeaveRequests.Select(l =>
        new LeaveRequestVM()
        {
            Id = l.Id,
            StartDate = System.DateOnly.Parse(l.StartDate),
            EndDate = System.DateOnly.Parse(l.EndDate),
            LeaveTypeId = l.LeaveTypeId,
            LeaveTypeName = l.LeaveTypeName,
            EmployeeFullName = l.EmployeeFullName,
            DateCreated = l.DateCreated,
            RequestComments = l.RequestComments,
            IsApproved = l.IsApproved,
            IsCancelled = l.IsCancelled
        }).ToArray();

        EmployeeLeaveRequestVM model = new(leaveRequests);

        return model;
    }

    public async Task<LeaveRequestVM> GetLeaveRequestAsync(Guid id)
    {
        LeaveRequestDetailsDto leaveRequest = await _client.LeaveRequestsGET3Async(id);
        return new LeaveRequestVM()
        {
            Id = leaveRequest.Id,
            StartDate = System.DateOnly.Parse(leaveRequest.StartDate),
            EndDate = System.DateOnly.Parse(leaveRequest.EndDate),
            LeaveTypeId = leaveRequest.LeaveTypeId,
            LeaveTypeName = leaveRequest.LeaveTypeName,
            EmployeeFullName = leaveRequest.EmployeeFullName,
            DateCreated = leaveRequest.DateCreated,
            RequestComments = leaveRequest.RequestComments,
            IsApproved = leaveRequest.IsApproved,
            IsCancelled = leaveRequest.IsCancelled
        };
    }

    public async Task<Response<Guid>> ApprovedLeaveRequestAsync(Guid id, bool status)
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

    public async Task<Response<Guid>> CancelLeaveRequestAsync(Guid id)
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
