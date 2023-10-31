using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Domain.Rents;
using Simbir.GO.Domain.Rents.Enums;

namespace Simbir.GO.Infrastructure.Persistence.Config;

public class RentConfig : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired();

        builder.Property(r => r.AccountId)
            .IsRequired();

        builder.Property(r => r.TransportId)
            .IsRequired();

        builder.Property(r => r.TimeStart)
            .IsRequired();

        builder.Property(r => r.TimeEnd);
        
        builder.Property(r => r.PriceOfUnit)
            .IsRequired();

        builder.Property(r => r.FinalPrice);
        
        builder.Property(r => r.PriceType).IsRequired()
            .HasConversion(
                r => r.ToString(), 
                r => (PriceType)Enum.Parse(typeof(PriceType), r));

     
    }
}