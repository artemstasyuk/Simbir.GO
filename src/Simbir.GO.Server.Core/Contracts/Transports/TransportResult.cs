using Simbir.GO.Server.ApplicationCore.Contracts.Rents;
using Simbir.GO.Server.Domain.Transports.ValueObjects;

namespace Simbir.GO.Server.ApplicationCore.Contracts.Transports;

public record TransportResult(
    long Id,
    long TransportOwnerId,
    string Model,
    string Color,
    string Identifier,
    string Description,
    Location Location,
    double MinutePrice,
    double DayPrice,
    bool CanBeRented,
    string TransportType,
    List <RentResult> TransportRents
);