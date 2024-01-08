using Blazored.LocalStorage;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Services.Base;

namespace CleanArch.BlazorUI.Services;

public class LeaveRequestService(IClient client, ILocalStorageService localStorage)
    : BaseHttpService(client, localStorage), ILeaveRequestService
{
}
