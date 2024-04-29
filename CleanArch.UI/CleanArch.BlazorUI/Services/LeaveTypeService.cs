using AutoMapper;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveTypeService(IClient client, IMapper mapper) : BaseHttpService(client), ILeaveTypeService
{
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<LeaveTypeVM>> GetLeaveTypeList()
    {
        LeaveTypeListDto leaveTypes = await _client.LeaveTypesGETAsync();
        return _mapper.Map<IReadOnlyCollection<LeaveTypeVM>>(leaveTypes.LeaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(Guid id)
    {
        LeaveTypeDetailDto leaveType = await _client.LeaveTypesGET2Async(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            CreateLeaveTypeRequest request = _mapper.Map<CreateLeaveTypeRequest>(leaveType);
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
            UpdateLeaveTypeRequest request = _mapper.Map<UpdateLeaveTypeRequest>(leaveType);
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
