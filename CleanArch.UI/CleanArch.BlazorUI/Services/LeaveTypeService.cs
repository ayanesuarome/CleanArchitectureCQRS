using AutoMapper;
using Blazored.LocalStorage;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorage)
    : BaseHttpService(client, localStorage), ILeaveTypeService
{
    private readonly IMapper _mapper = mapper;

    public async Task<List<LeaveTypeVM>> GetLeaveTypeList()
    {
        ICollection<LeaveTypeDto> leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        try
        {
            await AddBearerToken();
            LeaveTypeDetailsDto leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }
        catch (ApiException)
        {
            return null;
        }
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            await AddBearerToken();
            CreateLeaveTypeCommand command = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypesPOSTAsync(command);

            return new Response<Guid> { Success = true };
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
            await AddBearerToken();
            UpdateLeaveTypeCommand command = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypesPUTAsync(leaveType.Id, command);

            return new Response<Guid> { Success = true };
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<Guid> { Success = true };
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
