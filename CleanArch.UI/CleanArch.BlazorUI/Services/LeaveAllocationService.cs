using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveAllocationService(IClient client): BaseHttpService(client), ILeaveAllocationService
{
    public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
    {
        try
        {
            CreateLeaveAllocationCommand body = new()
            {
                LeaveTypeId = leaveTypeId
            };

            await _client.LeaveAllocationPOSTAsync(body);

            return new Response<Guid>();
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
