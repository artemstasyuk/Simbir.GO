using Ardalis.Specification;
using Simbir.GO.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public sealed class RentsByTransportSpec: Specification<Rent>
{
    public RentsByTransportSpec(long transportId) =>
        Query.Where(x => x.TransportId == transportId);
}