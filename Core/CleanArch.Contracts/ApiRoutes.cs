namespace CleanArch.Contracts;

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
        public const string GetById = "leave-types/{id}";
        public const string Post = "leave-types";
        public const string Put = "leave-types/{id}";
        public const string Delete = "leave-types/{id}";
    }

    /// <summary>
    /// Contains the leave request routes.
    /// </summary>
    public static class LeaveRequests
    {
        public const string Get = "leave-requests";
        public const string AdminGet = "admin-leave-requests";
        public const string GetById = "leave-requests/{id}";
        public const string Post = "leave-requests";
        public const string Put = "leave-requests/{id}";
        public const string Delete = "leave-requests/{id}";
        public const string CancelRequest = "leave-requests/{id}/cancel-request";
        public const string AdminUpdateApproval = "admin-leave-requests/{id}/update-approval";
    }

    /// <summary>
    /// Contains the leave request routes.
    /// </summary>
    public static class LeaveAllocations
    {
        public const string Get = "leave-allocations";
        public const string GetById = "leave-allocations/{id}";
        public const string Post = "leave-allocations";
        public const string Put = "leave-allocations/{id}";
        public const string Delete = "leave-allocations/{id}";
    }
}
