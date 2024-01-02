using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Tests.Features.Mocks;
using FluentValidation.TestHelper;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Shared;

public class BaseLeaveRequestCommandValidatorTest
{
    private readonly BaseLeaveRequestCommandValidator _validator;

    public BaseLeaveRequestCommandValidatorTest()
    {
        _validator = new BaseLeaveRequestCommandValidator();
    }

    [Theory]
    [ClassData(typeof(InvalidStringClassData))]
    public async Task TestValidatorShouldFailWithNullOrEmptyRequestingEmployeeId(string employeeId)
    {
        CreateLeaveRequestCommand command = new()
        {
            RequestingEmployeeId = employeeId
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.RequestingEmployeeId)
            .WithErrorMessage("Requesting Employee Id is required");
    }

    [Fact]
    public async Task TestValidatorShouldFailWhenStartDateIsGreaterThanEndDate()
    {
        UpdateLeaveRequestCommand command = new()
        {
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now,
        };

        var result = await _validator.TestValidateAsync(command);

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
            RequestingEmployeeId = "123",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
