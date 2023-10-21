using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Simbir.GO.Server.Infrastructure.Persistence.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.ConfigureOptions<PostgresOptionsSetup>();
        var databaseOptions = services.BuildServiceProvider().GetRequiredService<IOptions<PostgresOptions>>()!.Value;
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(databaseOptions.ConnectionString, npSqlServerAction =>
            {
                npSqlServerAction.EnableRetryOnFailure(maxRetryCount: databaseOptions.MaxRetryCount);
                npSqlServerAction.CommandTimeout(databaseOptions.CommandTimeOut);
            });
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            // TODO env if development
            options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors); // to get field-level error details 
            options.EnableSensitiveDataLogging(databaseOptions
                .EnableSensitiveDataLogging); // to get parameter values - do not in production!
            options.ConfigureWarnings(warningAction =>
            {
                warningAction.Log(CoreEventId.FirstWithoutOrderByAndFilterWarning, CoreEventId.RowLimitingOperationWithoutOrderByWarning);
            });
        });
        

        return services;
    }
}