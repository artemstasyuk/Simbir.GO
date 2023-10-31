namespace Simbir.GO.Server.ApplicationCore.Contracts.Admin.Rents;

public record AdminUpdateRentRequest(
    long TransportId,
    long UserId,
    string TimeStart,
    string? TimeEnd,
    double PriceOfUnit,
    string PriceType,
    double? FinalPrice
);