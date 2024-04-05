using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CleanArch.Application.Tests.Features.Mocks;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.Tests.Features.LeaveTypes.Commands.CreateLeaveTypes;

public class CreateLeaveTypeCommandValidatorTest(CreateLeaveTypeCommandValidatorFixture fixture)
    : IClassFixture<CreateLeaveTypeCommandValidatorFixture>
{
    private readonly CreateLeaveTypeCommandValidatorFixture _fixture = fixture;

    #region Tests

    [Theory]
    [ClassData(typeof(InvalidStringClassData))]
    public async Task TestValidatorShouldFailWithNullOrEmptyName(string name)
    {
        CreateLeaveType.Command command = new(name, 10);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage($"{nameof(CreateLeaveType.Command.Name)} is required");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameLengthGreaterThan70()
    {
        CreateLeaveType.Command command = new("somenamesomenamesomenamesomenamesomenamesomenamesomenamesomenamesomename", 10);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage($"{nameof(CreateLeaveType.Command.Name)} must be up to 70 characters");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameNotUnique()
    {
        CreateLeaveType.Command command = new("somename", 10);

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
        .ReturnsAsync(false);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Leave type already exist");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public async Task TestValidatorShouldFailWithDefaultDaysNotInRange1_100(int defaultDays)
    {
        CreateLeaveType.Command command = new("somename", defaultDays);

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
        .ReturnsAsync(true);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.DefaultDays)
            .WithErrorMessage("Default Days must be between 1 - 100");
    }

    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        CreateLeaveType.Command command = new("somename", 100);

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
        .ReturnsAsync(true);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion
}
