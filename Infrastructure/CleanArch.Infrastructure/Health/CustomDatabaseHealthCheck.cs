using CleanArch.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CleanArch.Infrastructure.Health;

internal sealed class CustomDatabaseHealthCheck(CleanArchEFDbContext dbContext)
    : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        bool canConnect = await dbContext.Database.CanConnectAsync();

        if(!canConnect)
        {
            return HealthCheckResult.Unhealthy(context.Registration.FailureStatus.ToString());
        }
        
        return HealthCheckResult.Healthy();
    }
}
