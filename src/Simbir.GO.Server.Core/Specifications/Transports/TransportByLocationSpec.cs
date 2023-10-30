using Ardalis.Specification;
using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Server.Domain.Transports;
using Simbir.GO.Server.Domain.Transports.ValueObjects;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Transports;

public class TransportByLocationSpec : Specification<Transport>
{
    public TransportByLocationSpec(TransportSearch search) =>
        Query.Where(transport =>
            Location.CalculateDistance(search.Latitude, search.Longitude, transport.Location) <= search.Radius
            //TODO 
            && transport.TransportType.ToString().Equals(search.TransportType, StringComparison.OrdinalIgnoreCase)
            && transport.CanBeRented);
}