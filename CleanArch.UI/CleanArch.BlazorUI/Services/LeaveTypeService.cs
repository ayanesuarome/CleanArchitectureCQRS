using AutoMapper;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveTypeService(IClient client, IMapper mapper)
    : BaseHttpService(client), ILeaveTypeService
{
    private readonly IMapper _mapper = mapper;

    public async Task<List<LeaveTypeVM>> GetLeaveTypeList()
    {
        ICollection<LeaveTypeDto> leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        //LeaveTypeDto leaveType = await _client.LeaveTypesGETAsync(id);
        throw new NotImplementedException();
    }

    public Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM leaveType)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Guid>> DeleteLeaveType(int id)
    {
        throw new NotImplementedException();
    }
}
