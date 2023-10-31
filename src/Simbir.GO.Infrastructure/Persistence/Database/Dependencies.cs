using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Simbir.GO.Infrastructure.Persistence.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Postgres"));
        var dataSource = dataSourceBuilder.Build();
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(dataSource);
            
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            
            options.ConfigureWarnings(warningAction =>
            {
                warningAction.Log(CoreEventId.FirstWithoutOrderByAndFilterWarning, CoreEventId.RowLimitingOperationWithoutOrderByWarning);
            });
        });
      
        return services;
    }
}