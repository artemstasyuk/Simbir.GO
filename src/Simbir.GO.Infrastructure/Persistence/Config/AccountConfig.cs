using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Domain.Accounts;
using Simbir.GO.Domain.Accounts.Enums;

namespace Simbir.GO.Infrastructure.Persistence.Config;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired();

        builder.HasMany(x => x.AccountRents)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId);

        builder.HasMany(x => x.AccountTransports)
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

    }
}