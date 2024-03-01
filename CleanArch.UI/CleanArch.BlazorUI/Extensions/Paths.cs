namespace CleanArch.BlazorUI.Extensions;

public static class Paths
{
    public const string Home = "/";

    public static class Identity
    {
        public const string Login = "login/";
        public const string Logout = "logout";
        public const string Register = "register/";
    }

    public static class LeaveType
    {
        public const string LeaveTypes = "leavetypes/";
        public const string CreateLeaveType = "/leavetypes/create/";
        public const string EditLeaveType = "/leavetypes/edit/{0}";
        public const string DetailsLeaveType = "/leavetypes/details/{0}";
    }

    public static class LeaveRequest
    {
        public const string LeaveRequests = "leaverequests/";
        public const string EmployeeLeaveRequests = "employee-leaverequests/";
        public const string CreateLeaveRequest = "/leaverequests/create/";
        public const string DetailsLeaveRequest = "/leaverequests/details/{0}";
    }
}
