using Microsoft.EntityFrameworkCore;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Rents;
using Simbir.GO.Server.Domain.Transports;


namespace Simbir.GO.Server.Infrastructure.Persistence;

public class AppDbContext : DbContext
{ 
    private DbSet<Account> Accounts { get; set; } = null!;
    
    private DbSet<Rent> Rents { get; set; } = null!;
    
    private DbSet<Transport> Transports { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCamelCaseNamingConvention();
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Inject all configurations of entities 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}