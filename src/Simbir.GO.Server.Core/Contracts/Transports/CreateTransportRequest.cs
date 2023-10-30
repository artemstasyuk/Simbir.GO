namespace Simbir.GO.Server.ApplicationCore.Contracts.Transports;

public record CreateTransportRequest(
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