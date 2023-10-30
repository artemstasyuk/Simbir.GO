using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Server.Domain.Transports;
using Simbir.GO.Server.Domain.Transports.Enums;
using Simbir.GO.Server.Infrastructure.Persistence.Database;

namespace Simbir.GO.Server.Infrastructure.Persistence.Config;

public class TransportConfig : IEntityTypeConfiguration<Transport>
{
    private readonly Transport _transport = SeedData.CreateTransport();
    
    public void Configure(EntityTypeBuilder<Transport> builder)
    {
        builder.ToTable("transports");

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
        
        builder.Property(tr => tr.Description)
            .IsRequired();

        builder.Property(tr => tr.MinutePrice)
            .IsRequired();

        builder.Property(tr => tr.DayPrice)
            .IsRequired();
        
        builder.Property(tr => tr.TransportType).IsRequired()
            .HasConversion(
                tr => tr.ToString(), 
                tr => (TransportType)Enum.Parse(typeof(TransportType), tr));
        
        builder.OwnsOne(tr => tr.Location);

        //TODO builder.HasData(_transport);
    }
}