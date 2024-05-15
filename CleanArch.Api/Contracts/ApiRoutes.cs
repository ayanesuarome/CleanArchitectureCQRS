namespace CleanArch.Api.Contracts;

/// <summary>
/// Contains the API endpoint routes.
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Contains the leave type routes.
    /// </summary>
    public static class LeaveTypes
    {
        public const string Get = "leave-types";
        public const string GetById = "leave-types/{id:guid}";
        public const string Post = "leave-types";
        public const string Put = "leave-types/{id:guid}";
        public const string Delete = "leave-types/{id:guid}";
    }

    /// <summary>
    /// Contains the leave request routes.
    /// </summary>
    public static class LeaveRequests
    {
        public const string Get = "leave-requests";
        public const string GetById = "leave-requests/{id:guid}";
        public const string Post = "leave-requests";
        public const string Put = "leave-requests/{id:guid}";
        public const string Delete = "leave-requests/{id:guid}";
        public const string CancelRequest = "leave-requests/{id:guid}/cancel-request";
        public const string AdminUpdateApproval = "leave-requests/{id:guid}/update-approval";
    }

    /// <summary>
    /// Contains the leave allocation routes.
    /// </summary>
    public static class LeaveAllocations
    {
        public const string Get = "leave-allocations";
        public const string GetById = "leave-allocations/{id:guid}";
        public const string Post = "leave-allocations";
        public const string Put = "leave-allocations/{id:guid}";
        public const string Delete = "leave-allocations/{id:guid}";
    }

    /// <summary>
    /// Contains the authentication routes.
    /// </summary>
    public static class Authentication
    {
        public const string Login = "authentication/login";
        public const string Register = "authentication/register";
    }
}
