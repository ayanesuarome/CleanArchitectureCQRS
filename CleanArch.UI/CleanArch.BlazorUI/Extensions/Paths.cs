namespace CleanArch.BlazorUI.Extensions;

public static class Paths
{
    public const string Home = "Home";

    public static class Identity
    {
        public const string Login = "login/";
        public const string Register = "register/";
    }

    public static class LeaveType
    {
        public const string CreateLeaveType = "/leavetypes/create/";
        public const string EditLeaveType = "/leavetypes/edit/{0}";
        public const string DetailsLeaveType = "/leavetypes/details/{0}";
    }
}
