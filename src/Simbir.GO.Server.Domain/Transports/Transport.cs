using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Common.Entity;
using Simbir.GO.Server.Domain.Rents;
using Simbir.GO.Server.Domain.Transports.Enums;
using Simbir.GO.Server.Domain.Transports.ValueObjects;

namespace Simbir.GO.Server.Domain.Transports;

/// <summary>
/// Represents a transport entity
/// </summary>
public class Transport : AggregateRoot
{
    /// <summary>
    /// Private field for updating entity field TransportRents
    /// </summary>
    private readonly List<Rent> _transportRents = new();
    
    /// <summary>
    /// Gets or sets the owner account identifier
    /// </summary>
    public long TransportOwnerId { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport model 
    /// </summary>
    public string Model { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport color 
    /// </summary>
    public string Color { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport identifier 
    /// </summary>
    public string Identifier { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport description 
    /// </summary>
    public string? Description { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport location 
    /// </summary>
    public Location Location { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport rent minute price 
    /// </summary>
    public double? MinutePrice { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport rent day price 
    /// </summary>
    public double? DayPrice { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport can be rented flag
    /// </summary>
    public bool CanBeRented { get; private set; }
    
    /// <summary>
    /// Gets or sets a transport type  
    /// </summary>
    public TransportType  TransportType { get; private set; }
    
    /// <summary>
    /// Read only list of transport rents
    /// </summary>
    public IReadOnlyList<Rent> TransportRents => _transportRents.AsReadOnly();
    
    
    public Account Account { get; set; }
    
    /// <summary>
    /// Ctor
    /// </summary>
    public Transport(long transportOwnerId, TransportType transportType, string model, string color,
        string identifier, string? description, Location location, double? minutePrice, double? dayPrice, bool canBeRented)
    {
        TransportOwnerId = transportOwnerId;
        TransportType = transportType;
        Model = model;
        Color = color;
        Identifier = identifier;
        Description = description;
        Location = location;
        MinutePrice = minutePrice;
        DayPrice = dayPrice;
        CanBeRented = canBeRented;
    }

    public static Transport Create(long transportOwnerId, TransportType transportType, string model, string color,
        string identifier, string? description, Location location, double? minutePrice, double? dayPrice, bool canBeRented) =>
        new(transportOwnerId, transportType, model, color, identifier, description, location, minutePrice, dayPrice, canBeRented);
    
    public Transport Update(long transportOwnerId, TransportType transportType, string model, string color,
        string identifier, string? description, Location location, double? minutePrice, double? dayPrice, bool canBeRented)
    {

        TransportOwnerId = transportOwnerId;
        TransportType = transportType;
        Model = model;
        Color = color;
        Identifier = identifier;
        Description = description;
        Location = location;
        MinutePrice = minutePrice;
        DayPrice = dayPrice;
        CanBeRented = canBeRented;

        return this;
    }

    public void AddRent(Rent rent) =>
        _transportRents.Add(rent);
    
    
#pragma warning disable CS8618
    public Transport(){}
#pragma warning restore CS8618 
}
