using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Authentication;
using CleanArch.Domain.Core.Primitives.Result;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    internal sealed class Handler : ICommandHandler<Command, Result<Response>>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<Response>> Handle(Command command, CancellationToken cancellationToken)
        {
            Result<UserName> firstNameResult = UserName.Create(command.FirstName);
            Result<UserName> lastNameResult = UserName.Create(command.LastName);
            Result<Email> emailResult = Email.Create(command.Email);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, emailResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure<Response>(firstFailureOrSuccess.Error);
            }
            
            User user = new(firstNameResult.Value, lastNameResult.Value)
            {
                Email = emailResult.Value,
                UserName = emailResult.Value,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return Result.Failure<Response>(ValidationErrors.CreateUser.CreateUserValidation(result.Errors.ToString()));
            }

            await _userManager.AddToRoleAsync(user, Roles.Employee.Name);

            Response response = new(user.Id);

            return Result.Success<Response>(response);
        }
    }
}
