namespace Simbir.GO.Server.ApplicationCore.Contracts.Admin.Transports;

public record AdminCreateTransportRequest(
    long OwnerId,
    bool CanBeRented,
    string TransportType,
    string Model,
    string Color,
    string Identifier,
    string? Description,
    double Latitude,
    double Longitude,
    double? MinutePrice,
    double? DayPrice
);