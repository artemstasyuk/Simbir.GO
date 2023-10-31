using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Domain.Transports;
using Simbir.GO.Domain.Transports.Enums;

namespace Simbir.GO.Infrastructure.Persistence.Config;

public class TransportConfig : IEntityTypeConfiguration<Transport>
{
    public void Configure(EntityTypeBuilder<Transport> builder)
    {
        builder.HasKey(transport => transport.Id);

        builder.Property(transport => transport.Id)
            .IsRequired();

        builder.HasMany(x => x.TransportRents)
            .WithOne(x => x.Transport)
            .HasForeignKey(x => x.TransportId);
        
        builder.Property(tr => tr.TransportOwnerId)
            .IsRequired();

        builder.Property(tr => tr.Model)
            .IsRequired();
        
        builder.Property(tr => tr.Color)
            .IsRequired();
        
        builder.Property(tr => tr.Identifier)
            .IsRequired();

        builder.Property(tr => tr.Description);

        builder.Property(tr => tr.MinutePrice);

        builder.Property(tr => tr.DayPrice);

        builder.OwnsOne(tr => tr.Location);
        
        
        builder.Property(tr => tr.TransportType).IsRequired()
            .HasConversion(
                tr => tr.ToString(), 
                tr => (TransportType)Enum.Parse(typeof(TransportType), tr));
    }
}