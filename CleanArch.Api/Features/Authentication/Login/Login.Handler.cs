using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Api.Features.Authentication.Login;

public static partial class Login
{
    public sealed class Handler : IRequestHandler<Command, Result<TokenResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IValidator<Command> _validator;

        public Handler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtProvider jwtProvider,
        IValidator<Command> validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _validator = validator;
        }

        public async Task<Result<TokenResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new FailureResult<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return new FailureResult<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            string securityToken = await _jwtProvider.GenerateToken(user);
            TokenResponse response = new(securityToken);

            return new SuccessResult<TokenResponse>(response);
        }
    }
}
