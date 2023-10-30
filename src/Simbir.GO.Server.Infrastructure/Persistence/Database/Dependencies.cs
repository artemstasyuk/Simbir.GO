using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Simbir.GO.Server.Infrastructure.Persistence.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        services.ConfigureOptions<PostgresOptionsSetup>();
        var databaseOptions = services.BuildServiceProvider().GetRequiredService<IOptions<PostgresOptions>>()!.Value;
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(databaseOptions.ConnectionString, npSqlServerAction =>
            {
                npSqlServerAction.CommandTimeout(databaseOptions.CommandTimeOut);
            });
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            
            options.ConfigureWarnings(warningAction =>
            {
                warningAction.Log(CoreEventId.FirstWithoutOrderByAndFilterWarning, CoreEventId.RowLimitingOperationWithoutOrderByWarning);
            });
        });

        var db = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
        db.Database.MigrateAsync();
        
        return services;
    }
}