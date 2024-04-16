using AutoMapper;
using CleanArch.Api.Features.Authentication;
using CleanArch.Api.Features.LeaveAllocations;
using CleanArch.Api.Features.LeaveRequests;
using CleanArch.Api.Features.LeaveTypes;

namespace CleanArch.Application.Tests.AutoMapper;

/// <summary>
/// Test class to test the mapper configuration.
/// </summary>
public class MapperConfigurationTests
{
    #region Test Methods

    /// <summary>
    /// Tests the mapper profiles configuration.
    /// </summary>
    /// <param name="profileType">Profile type.</param>
    [Theory, MemberData(nameof(GetMapperProfiles))]
    public void TestAutoMapperProfilesConfiguration(Type profileType)
    {
        MapperConfiguration configuration = new(cfg => cfg.AddProfile(profileType));
        configuration.AssertConfigurationIsValid();
    }

    #endregion

    #region Member Test Data

    public static IEnumerable<object[]> GetMapperProfiles()
    {
        yield return new object[] { typeof(LeaveTypeProfile) };
        yield return new object[] { typeof(LeaveAllocationProfile) };
        yield return new object[] { typeof(LeaveRequestProfile) };
        yield return new object[] { typeof(AuthenticationProfile) };
    }

    #endregion
}
