﻿using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Extensions;

public static class NavigationManagerExtension
{
    public static void NavigateToHome(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(Paths.Home);
    }
    
    public static void NavigateToLogin(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(Paths.Identity.Login);
    }
    
    public static void NavigateToRegister(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(Paths.Identity.Register);
    }

    #region Leave Types

    public static void NavigateToIndexLeaveType(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(Paths.LeaveType.LeaveTypes);
    }

    public static void NavigateToCreateLeaveType(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(Paths.LeaveType.CreateLeaveType);
    }
    
    public static void NavigateToDetailsLeaveType(this NavigationManager navigationManager, Guid id)
    {
        navigationManager.NavigateTo(String.Format(Paths.LeaveType.DetailsLeaveType, id));
    }
    
    public static void NavigateToEditLeaveType(this NavigationManager navigationManager, Guid id)
    {
        navigationManager.NavigateTo(String.Format(Paths.LeaveType.EditLeaveType, id));
    }

    #endregion

    #region Leave Requests

    public static void NavigateToIndexLeaveRequest(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(Paths.LeaveRequest.LeaveRequests);
    }
    public static void NavigateToEmployeeIndexLeaveRequest(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo(Paths.LeaveRequest.EmployeeLeaveRequests);
    }
    public static void NavigateToDetailsLeaveRequest(this NavigationManager navigationManager, Guid id)
    {
        navigationManager.NavigateTo(String.Format(Paths.LeaveRequest.DetailsLeaveRequest, id));
    }

    #endregion
}
