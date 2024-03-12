using AutoMapper;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveTypeService(IClient client, IMapper mapper) : BaseHttpService(client), ILeaveTypeService
{
    private readonly IMapper _mapper = mapper;

    public async Task<List<LeaveTypeVM>> GetLeaveTypeList()
    {
        ICollection<LeaveTypeDto> leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        LeaveTypeDetailsDto leaveType = await _client.LeaveTypesGETAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            CreateLeaveTypeCommand command = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypesPOSTAsync(command);

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
            UpdateLeaveTypeCommand command = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypesPUTAsync(leaveType.Id, command);

            return new Response<Guid>();
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
            await _client.AdminLeaveTypeAsync(id);
            return new Response<Guid>();
        }
        catch(ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
