namespace Simbir.GO.Server.ApplicationCore.Contracts.Transports;

public record TransportSearch(
    double Latitude, 
    double Longitude, 
    double Radius,
    string TransportType
);