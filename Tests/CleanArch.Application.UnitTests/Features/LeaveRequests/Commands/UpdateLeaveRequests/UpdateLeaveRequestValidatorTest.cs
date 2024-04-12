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
        UpdateLeaveRequest.Command command = new()
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage($"The Id is required.");
    }
    
    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        UpdateLeaveRequest.Command command = new()
        {
            Id = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
