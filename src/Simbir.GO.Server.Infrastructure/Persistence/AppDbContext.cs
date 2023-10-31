using Microsoft.EntityFrameworkCore;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Domain.Rents;
using Simbir.GO.Server.Domain.Rents.Enums;
using Simbir.GO.Server.Domain.Transports;
using Simbir.GO.Server.Domain.Transports.Enums;


namespace Simbir.GO.Server.Infrastructure.Persistence;

public class AppDbContext : DbContext
{ 
    public DbSet<Account> Accounts { get; set; } = null!;
    
    public DbSet<Rent> Rents { get; set; } = null!;
    
    public DbSet<Transport> Transports { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCamelCaseNamingConvention();
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Postgres enums 
        modelBuilder.HasPostgresEnum<PriceType>();
        modelBuilder.HasPostgresEnum<TransportType>();
        modelBuilder.HasPostgresEnum<Role>();
        
        // Inject all configurations of entities 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}