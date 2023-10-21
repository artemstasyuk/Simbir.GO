using Microsoft.EntityFrameworkCore;


namespace Simbir.GO.Server.Infrastructure.Persistence;

public class AppDbContext : DbContext
{ 
    // Main bl models 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Inject all configurations of entities 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}