using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Domain.Rents.Enums;
using Simbir.GO.Server.Domain.Transports.Enums;

namespace Simbir.GO.Server.Infrastructure.Persistence.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Postgres"));
       
        dataSourceBuilder.MapEnum<Role>();
        dataSourceBuilder.MapEnum<TransportType>();
        dataSourceBuilder.MapEnum<PriceType>();
        
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