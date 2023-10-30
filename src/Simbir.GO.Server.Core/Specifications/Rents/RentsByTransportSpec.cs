using Ardalis.Specification;
using Simbir.GO.Server.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public class RentsByTransportSpec: Specification<Rent>
{
    public RentsByTransportSpec(long transportId) =>
        Query.Where(x => x.TransportId == transportId);
}