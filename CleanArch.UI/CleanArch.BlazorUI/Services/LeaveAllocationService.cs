using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveAllocationService(IClient client)
    : BaseHttpService(client), ILeaveAllocationService
{
}
