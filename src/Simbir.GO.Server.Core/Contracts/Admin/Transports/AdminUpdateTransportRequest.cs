namespace Simbir.GO.Server.ApplicationCore.Contracts.Admin.Transports;

public record AdminUpdateTransportRequest(
    long OwnerId,
    string TransportType,
    bool CanBeRented,
    string Model,
    string Color,
    string Identifier,
    string? Description,
    double Latitude,
    double Longitude,
    double? MinutePrice,
    double? DayPrice
);

