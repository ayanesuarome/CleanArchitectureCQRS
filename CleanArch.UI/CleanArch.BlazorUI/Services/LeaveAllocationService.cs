using Blazored.LocalStorage;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveAllocationService(IClient client, ILocalStorageService localStorage)
    : BaseHttpService(client, localStorage), ILeaveAllocationService
{
    public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
    {
        try
        {
            await AddBearerToken();
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
