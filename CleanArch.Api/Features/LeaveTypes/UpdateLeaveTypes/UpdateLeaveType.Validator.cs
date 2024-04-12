using CleanArch.Application.Extensions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            RuleFor(m => m.Id)
                .NotEmpty()
                .WithError(LeaveTypeErrors.IdRequired());

            RuleFor(m => m.Name)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(70)
                    .WithError(LeaveTypeErrors.NameMaximumLength("{MaxLength}"))
                .MustAsync(LeaveTypeUniqueName)
                    .WithError(LeaveTypeErrors.NameUnique());

            RuleFor(m => m.DefaultDays)
                .InclusiveBetween(1, 100)
                    .WithError(LeaveTypeErrors.DefaultDaysRange("{From} - {To}"));
        }

        private async Task<bool> LeaveTypeUniqueName(Command command, string name, CancellationToken cancellation)
        {
            if(!string.IsNullOrEmpty(name))
            {
                LeaveType leaveType = await _repository.GetByIdAsync(command.Id);

                if (leaveType.Name != name)
                {
                    return await _repository.IsUniqueAsync(name, cancellation);
                }
            }           

            return true;
        }
    }
}
