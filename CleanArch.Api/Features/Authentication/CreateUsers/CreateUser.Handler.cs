using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed class Handler : IRequestHandler<Command, Result<RegistrationResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IValidator<Command> _validator;

        public Handler(
        UserManager<ApplicationUser> userManager,
        IJwtProvider jwtProvider,
        IValidator<Command> validator)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _validator = validator;
        }

        public async Task<Result<RegistrationResponse>> Handle(Command command, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return new FailureResult<RegistrationResponse>(ValidationErrors.CreateUser.CreateUserValidation(validationResult.ToString()));
            }

            ApplicationUser user = new(command.FirstName, command.LastName)
            {
                Email = command.Email,
                UserName = command.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                StringBuilder errors = new();

                foreach (IdentityError error in result.Errors)
                {
                    errors.AppendFormat("-{0}\n", error.Description);
                }
                return new FailureResult<RegistrationResponse>(ValidationErrors.CreateUser.CreateUserValidation(result.Errors.ToString()));
            }

            await _userManager.AddToRoleAsync(user, Roles.Employee);

            RegistrationResponse response = new(user.Id);

            return new SuccessResult<RegistrationResponse>(response);
        }
    }
}
