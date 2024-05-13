using CleanArch.Identity.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Identity.ConfigureOptions;

internal sealed class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration = configuration;

    public void Configure(JwtOptions options)
    {
        _configuration
            .GetRequiredSection(SectionName)
            .Bind(options);
    }
}
