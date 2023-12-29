using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidatorTest : IDisposable
{
    private CreateLeaveRequestCommandValidator validator;
    private Mock<ILeaveTypeRepository> repositoryMock;

    #region Setup and Cleanup

    public CreateLeaveRequestCommandValidatorTest()
    {
        repositoryMock = new Mock<ILeaveTypeRepository>();
        validator = new CreateLeaveRequestCommandValidator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }

    #endregion

    #region Tests

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TestValidatorShouldFailWithLeaveTypeIdLessThanOrEqualTo0(int leaveTypeId)
    {
        CreateLeaveRequestCommand command = new()
        {
            LeaveTypeId = leaveTypeId
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.LeaveTypeId)
            .WithErrorMessage("Leave Type Id should be greather than 0");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithLeaveTypeDoesNotExist()
    {
        CreateLeaveRequestCommand command = new()
        {
            LeaveTypeId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
        };

        repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(() => null);

        var result = await validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.LeaveTypeId)
            .WithErrorMessage("Leave Type Id does not exist");
    }

    [Fact]
    public async Task TestValidatorShouldFailWhenStartDateIsGreaterThanEndDate()
    {
        CreateLeaveRequestCommand command = new()
        {
            LeaveTypeId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now,
        };

        repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new LeaveType());

        var result = await validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithoutErrorMessage("Start Date must be before End Date");
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithoutErrorMessage("End Date must be after Start Date");
    }

    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        CreateLeaveRequestCommand command = new()
        {
            LeaveTypeId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
        };

        repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new LeaveType());

        var result = await validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion
}
