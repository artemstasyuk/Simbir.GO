using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Infrastructure.Persistence.Database;

namespace Simbir.GO.Server.Infrastructure.Persistence.Config;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    private readonly List<Account> _accounts = SeedData.CreateUsers();
    
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.HasMany(x => x.AccountRents)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId);

        builder.HasMany(x => x.AccountTransport)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.TransportOwnerId);

        builder.Property(p => p.Username)
            .IsRequired();

        builder.Property(p => p.PasswordHash)
            .IsRequired();

        builder.Property(p => p.PasswordSalt)
            .IsRequired();
        
        builder.Property(p => p.Balance)
            .IsRequired();
        
        builder.Property(p => p.PasswordHash)
            .IsRequired();
        
        builder.Property(p => p.CreatedDateTime)
            .IsRequired();
        
        builder.Property(p => p.UpdatedDateTime)
            .IsRequired();
        
        builder.Property(p => p.Role).IsRequired()
            .HasConversion(
                p => p.ToString(),
                p => (Role)Enum.Parse(typeof(Role), p));
        
       //TODO builder.HasData(_accounts);
        
    }
}