﻿using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveAllocationService(IClient client): BaseHttpService(client), ILeaveAllocationService
{
    public async Task<Response<Guid>> CreateLeaveAllocations(Guid leaveTypeId)
    {
        try
        {
            CreateLeaveAllocation_Request request = new()
            {
                LeaveTypeId = leaveTypeId
            };

            await _client.LeaveAllocationsPOSTAsync(request);

            return new Response<Guid>();
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
