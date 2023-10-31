using Microsoft.EntityFrameworkCore;
using Simbir.GO.Domain.Accounts;
using Simbir.GO.Domain.Rents;
using Simbir.GO.Domain.Transports;
using Simbir.GO.Server.ApplicationCore.Auth;


namespace Simbir.GO.Infrastructure.Persistence;

public class AppDbContext : DbContext
{ 
    public DbSet<Account> Accounts { get; set; } = null!;
    
    public DbSet<Rent> Rents { get; set; } = null!;
    
    public DbSet<Transport> Transports { get; set; } = null!;
    

    public DbSet<RevokedToken> RevokedTokens { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSnakeCaseNamingConvention();
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Inject all configurations of entities 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}