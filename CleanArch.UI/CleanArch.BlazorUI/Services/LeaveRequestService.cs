using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveRequestService(IClient client)
    : BaseHttpService(client), ILeaveRequestService
{
}
