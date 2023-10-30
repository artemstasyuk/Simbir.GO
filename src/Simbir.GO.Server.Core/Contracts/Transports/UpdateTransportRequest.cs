namespace Simbir.GO.Server.ApplicationCore.Contracts.Transports;

public record UpdateTransportRequest(
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
  
