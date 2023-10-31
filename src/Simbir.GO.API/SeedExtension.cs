using Serilog;
using Simbir.GO.Infrastructure.Persistence;
using Simbir.GO.Infrastructure.Persistence.Database;

namespace Simbir.GO.API;

public static class SeedExtension
{
    public static async Task AddSeed(this IServiceCollection services)
    {
        Log.Information("Seeding Database...");
        
        try
        {
            var context = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
            await AppDbContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred seeding the DB.");
        }
    }
}
