using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Server.ApplicationCore.Auth;

namespace Simbir.GO.Infrastructure.Persistence.Config;

public class RevokedTokenConfig : IEntityTypeConfiguration<RevokedToken>
{
    public void Configure(EntityTypeBuilder<RevokedToken> builder)
    {

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired();
        
        builder.Property(p => p.Token)
            .IsRequired();
        
        builder.Property(p => p.JwtId)
            .IsRequired();
        
        builder.Property(p => p.UserId)
            .IsRequired();
        
        builder.Property(p => p.IsRevoked)
            .IsRequired();
        
        builder.Property(p => p.CreatedDateTime)
            .IsRequired();
        
        builder.HasOne(x => 
            x.Account);
    }
}