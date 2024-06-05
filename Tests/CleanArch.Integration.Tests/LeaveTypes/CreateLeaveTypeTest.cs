using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Integration.Tests.LeaveTypes;

public class CreateLeaveTypeTest : BaseIntegrationTest
{
    public CreateLeaveTypeTest(IntegrationTestWebAppFactory factory)
        :base(factory)
    {
    }

    [Fact]
    public async Task HandleShouldAdd_NewLeaveTypeToDatabase()
    {
        // Arrange
        CreateLeaveType.Command command = new("Vacation", 20);

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        Assert.True(result.IsSuccess);
        LeaveType? leaveType = DbContext.LeaveTypes.FirstOrDefault(leave => leave.Id == result.Value);
        Assert.NotNull(leaveType);
    }
}
