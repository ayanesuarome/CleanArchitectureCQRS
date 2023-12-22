using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.CreateLeaveTypes;

public class CreateLeaveTypeCommandValidatorTest : IDisposable
{
    private CreateLeaveTypeCommandValidator validator;
    private Mock<ILeaveTypeRepository> repositoryMock;

    #region Setup and Cleanup

    public CreateLeaveTypeCommandValidatorTest()
    {
        repositoryMock = new Mock<ILeaveTypeRepository>();
        validator = new CreateLeaveTypeCommandValidator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }

    #endregion

    #region Tests

    [Theory]
    [MemberData(nameof(GetInvalidStrings))]
    public void TestValidatorShouldFailWithNullOrEmptyName(string name)
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = name
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name is required");
    }
    
    [Fact]
    public void TestValidatorShouldFailWithNameLengthGreatherThan70()
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = "somenamesomenamesomenamesomenamesomenamesomenamesomenamesomenamesomename"
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name must be up to 70 characters");
    }

    [Fact]
    public async Task TestValidatorShouldNotFailWithNameLengthLessThan70()
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = "somename"
        };

        repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
            .ReturnsAsync(true);

        var result = await validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameNotUnique()
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = "somename"
        };

        repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
            .ReturnsAsync(false);

        var result = await validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Leave type already exist");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public async Task TestValidatorShouldFailWithDefaultDaysNotInRange1_100(int defaultDays)
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = "somename",
            DefaultDays = defaultDays
        };

        repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
            .ReturnsAsync(true);

        var result = await validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.DefaultDays)
            .WithErrorMessage("Default Days must be between 1 - 100");
    }

    #endregion

    #region Member Test Data

    public static IEnumerable<object[]> GetInvalidStrings()
    {
        yield return new object[] { null };
        yield return new object[] { string.Empty };
        yield return new object[] { " " };
    }

    #endregion
}
