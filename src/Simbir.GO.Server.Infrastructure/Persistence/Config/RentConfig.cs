using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Server.Domain.Rents;
using Simbir.GO.Server.Domain.Rents.Enums;

namespace Simbir.GO.Server.Infrastructure.Persistence.Config;

public class RentConfig : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {
        builder.ToTable("rents");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired();

        builder.Property(r => r.AccountId)
            .IsRequired();

        builder.Property(r => r.TransportId)
            .IsRequired();

        builder.Property(r => r.TimeStart)
            .IsRequired();
        
        builder.Property(r => r.TimeEnd)
            .IsRequired();
        
        builder.Property(r => r.PriceOfUnit)
            .IsRequired();
        
        builder.Property(r => r.FinalPrice)
            .IsRequired();

        builder.Property(r => r.PriceType).IsRequired()
            .HasConversion(
                r => r.ToString(),
                r => (PriceType)Enum.Parse(typeof(PriceType), r));
    }
}