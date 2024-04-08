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
}
