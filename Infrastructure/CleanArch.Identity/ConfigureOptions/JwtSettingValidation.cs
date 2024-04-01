using CleanArch.Application.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Identity.ConfigureOptions;

internal sealed class JwtSettingValidation : IValidateOptions<JwtSettings>
{
    private const string SectionName = nameof(JwtSettings);
    public readonly JwtSettings Configuration;

    public JwtSettingValidation(IConfiguration configuration)
    {
        Configuration = configuration
            .GetSection(SectionName)
            .Get<JwtSettings>();
    }

    public ValidateOptionsResult Validate(string? name, JwtSettings options)
    {
        if (options.DurationInMinutes < 60)
        {
            return ValidateOptionsResult.Fail($"{options.DurationInMinutes} must be greather or equal to 60");
        }

        return ValidateOptionsResult.Success;
    }
}
