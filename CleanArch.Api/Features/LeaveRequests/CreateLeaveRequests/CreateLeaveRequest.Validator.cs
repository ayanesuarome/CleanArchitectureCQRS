using CleanArch.Application.Extensions;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            RuleFor(m => m.LeaveTypeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                    .WithError(ValidationErrors.CreateLeaveRequest.LeaveTypeIdIsRequired)
                .MustAsync(LeaveTypeMustExist)
                    .WithError(DomainErrors.LeaveRequest.LeaveTypeMustExist);

            RuleFor(m => m.RequestComments)
                .MaximumLength(300)
                .WithError(ValidationErrors.CreateLeaveRequest.RequestCommentsMaximumLength("{MaxLength}"));

            RuleFor(m => m.StartDate)
                .LessThan(m => m.EndDate)
                .WithError(ValidationErrors.CreateLeaveRequest.StartDateLowerThanEndDate);

            RuleFor(m => m.EndDate)
                .GreaterThan(m => m.StartDate)
                .WithError(ValidationErrors.CreateLeaveRequest.EndDateGeatherThanStartDate);
            
            //Include(new BaseCommandValidator());
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            return await _repository.AnyAsync(id, token);
        }
    }
}
