using CleanArch.Application.Exceptions;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Identity.Interfaces;
using CleanArch.Identity.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Text;
using CleanArch.Contracts.Identity;

namespace CleanArch.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator<RegistrationRequest> _validator;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IValidator<RegistrationRequest> validator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

        if(user is null)
        {
            throw new BadRequestException("Invalid email or password");
        }

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        
        if(!result.Succeeded)
        {
            throw new BadRequestException("Invalid email or password");
        }

        string securityToken = await _jwtTokenGenerator.GenerateToken(user);

        return new AuthResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = securityToken
        };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid registration", validationResult);
        }

        ApplicationUser user = new()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if(!result.Succeeded)
        {
            StringBuilder errors = new();

            foreach (IdentityError error in result.Errors)
            {
                errors.AppendFormat("-{0}\n", error.Description);
            }
            throw new BadRequestException(result.Errors.ToString());
        }

        await _userManager.AddToRoleAsync(user, Roles.Employee);

        return new RegistrationResponse(user.Id);
    }
}
