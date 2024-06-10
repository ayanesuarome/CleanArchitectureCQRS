using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;
using FluentValidation.TestHelper;

namespace CleanArch.Application.UnitTests.Features.LeaveRequests.Commands.UpdateLeaveRequests;

public class UpdateLeaveRequestValidatorTest(UpdateLeaveRequestValidatorFixture fixture)
    : IClassFixture<UpdateLeaveRequestValidatorFixture>
{
    private readonly UpdateLeaveRequestValidatorFixture _fixture = fixture;

    [Fact]
    public async Task TestValidatorShouldFailWithInvalidId()
    {
        UpdateLeaveRequest.Command command = new(
            Guid.Empty,
            "06/06/2024",
            "06/07/2024",
            null);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage($"The Id is required.");
    }
    
    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        UpdateLeaveRequest.Command command = new(
            Guid.NewGuid(),
            "06/06/2024",
            "06/07/2024",
            null);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
