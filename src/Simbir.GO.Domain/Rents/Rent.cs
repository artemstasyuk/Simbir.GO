using Simbir.GO.Domain.Accounts;
using Simbir.GO.Domain.Common.Entity;
using Simbir.GO.Domain.Rents.Enums;
using Simbir.GO.Domain.Transports;

namespace Simbir.GO.Domain.Rents;

/// <summary>
/// Represents a rent entity
/// </summary>
public class Rent : AggregateRoot
{
    /// <summary>
    /// Gets or sets the transport identifier
    /// </summary>
    public long TransportId { get; private set; }
    
    /// <summary>
    /// Gets or sets the account identifier
    /// </summary>
    public long AccountId { get; private set; }
    
    /// <summary>
    /// Gets or sets the rent type
    /// </summary>
    public PriceType PriceType { get; private set; }
    
    /// <summary>
    /// Gets or sets the rent start time 
    /// </summary>
    public DateTime TimeStart { get; private set; }
    
    /// <summary>
    /// Gets or sets the rent end time 
    /// </summary>
    public DateTime? TimeEnd { get; private set; }
    
    /// <summary>
    ///Gets or sets transport price per unit of time 
    /// </summary>
    public double PriceOfUnit { get; private set; }
    
    /// <summary>
    ///Gets or sets final rent price
    /// </summary>
    public double? FinalPrice { get; private set; }
    
    
    public Account? Account { get; set; } 
    public Transport? Transport { get; set; }
    
    /// <summary>
    /// Ctor
    /// </summary>
    public Rent(long transportId, long accountId, PriceType priceType,
        double priceOfUnit, DateTime timeStart, DateTime? timeEnd = null, double? finalPrice = null) 
    {
        TransportId = transportId;
        AccountId = accountId;
        PriceType = priceType;
        PriceOfUnit = priceOfUnit;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
        FinalPrice = finalPrice;
    }
    
    public static Rent Create(long transportId, long accountId, PriceType priceType,
        double priceOfUnit, DateTime timeStart, DateTime? timeEnd, double? finalPrice) =>
        new(transportId, accountId, priceType, priceOfUnit, timeStart, timeEnd, finalPrice);
    
    public Rent Update(long transportId, long accountId, PriceType priceType,
        double priceOfUnit, DateTime timeStart, DateTime? timeEnd, double? finalPrice)
    {
        TransportId = transportId;
        AccountId = accountId;
        PriceType = priceType;
        PriceOfUnit = priceOfUnit;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
        FinalPrice = finalPrice;

        return this;
    }
    
    public static Rent Start(long transportId, long accountId, PriceType priceType, double priceOfUnit)
        => new ( transportId, accountId, priceType, priceOfUnit, DateTime.UtcNow);
    
    public Rent End()
    {
        TimeEnd = DateTime.UtcNow;
        FinalPrice = CalculatePrice();

        return this;
    }
    
    private double CalculatePrice()
    {
        double finalPrice = 0;
        
        switch (PriceType)
        {
            case PriceType.Minutes:
                TimeSpan durationMinutes = TimeEnd!.Value - TimeStart;
                finalPrice = PriceOfUnit * durationMinutes.TotalMinutes;
                break;
            case PriceType.Days:
                TimeSpan durationDays = TimeEnd!.Value.Date - TimeStart.Date;
                finalPrice = PriceOfUnit * (durationDays.Days + 1);
                break;
        }

        return finalPrice;
    }
}