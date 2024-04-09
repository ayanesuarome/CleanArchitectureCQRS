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
        public const string GetById = "leave-requests/{id}";
        public const string Post = "leave-requests";
        public const string Put = "leave-requests/{id}";
        public const string Delete = "leave-requests/{id}";
    }
}
