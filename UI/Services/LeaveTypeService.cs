using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveTypeService(IClient client) : BaseHttpService(client), ILeaveTypeService
{
    public async Task<IReadOnlyCollection<LeaveTypeVM>> GetLeaveTypeList()
    {
        LeaveTypeListDto dto = await _client.LeaveTypesGETAsync();
        List<LeaveTypeVM> models = new();

        foreach (LeaveTypeModel leaveType in dto.LeaveTypes)
        {
            models.Add(new()
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                DefaultDays = leaveType.DefaultDays
            });
        }

        return models;
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(Guid id)
    {
        LeaveTypeDetailDto leaveType = await _client.LeaveTypesGET2Async(id);
        return new LeaveTypeVM()
        {
            Id = leaveType.Id,
            Name = leaveType.Name,
            DefaultDays = leaveType.DefaultDays
        };
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            CreateLeaveTypeRequest request = new()
            {
                Name = leaveType.Name,
                DefaultDays = leaveType.DefaultDays
            };

            await _client.LeaveTypesPOSTAsync(request);

            return new Response<Guid>();
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            UpdateLeaveTypeRequest request = new()
            {
                Name = leaveType.Name,
                DefaultDays = leaveType.DefaultDays
            };
            await _client.LeaveTypesPUTAsync(leaveType.Id, request);

            return new Response<Guid>();
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(Guid id)
    {
        try
        {
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>();
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
