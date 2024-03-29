namespace Simbir.GO.Server.ApplicationCore.Contracts.Rents;

public record RentResult(
    long Id,
    long TransportId,
    long AccountId,
    string PriceType,
    string TimeStart,
    string? TimeEnd,
    double PriceOfUnit,
    double? FinalPrice
);