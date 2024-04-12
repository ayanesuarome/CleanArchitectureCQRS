using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.UpdateLeaveTypes;

public class UpdateLeaveTypeCommandValidatorTest(UpdateLeaveTypeCommandValidatorFixture fixture)
    : IClassFixture<UpdateLeaveTypeCommandValidatorFixture>
{
    private readonly UpdateLeaveTypeCommandValidatorFixture _fixture = fixture;

    #region Tests

    [Fact]
    public async Task TestValidatorShouldFailWithNullId()
    {
        UpdateLeaveType.Command command = new("name", 10);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage($"The {nameof(UpdateLeaveType.Command.Id)} is required.");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameLengthGreaterThan70()
    {
        UpdateLeaveType.Command command = new(
            "somenamesomenamesomenamesomenamesomenamesomenamesomenamesomenamesomename",
            10);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage($"The {nameof(UpdateLeaveType.Command.Name)} must be up to 70 characters.");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameNotUnique()
    {
        UpdateLeaveType.Command command = new("somename", 10);

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(command.Name, default))
            .ReturnsAsync(false);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Leave type already exist.");
    }

    [Fact]
    public async Task TestValidatorShouldNotFailWithNoNameToUpdate()
    {
        UpdateLeaveType.Command command = new(null, 100)
        {
            Id = 100,
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        UpdateLeaveType.Command command = new("Test Vacation", 100)
        {
            Id = 1,
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public async Task TestValidatorShouldFailWithDefaultDaysNotInRange1_100(int defaultDays)
    {
        UpdateLeaveType.Command command = new("Test Vacation", defaultDays)
        {
            Id = 1,
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.DefaultDays)
            .WithErrorMessage("The DefaultDays must be between 1 - 100.");
    }

    #endregion
}
