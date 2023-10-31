using Ardalis.Specification;
using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Domain.Transports;
using Simbir.GO.Domain.Transports.Enums;
using Simbir.GO.Domain.Transports.Errors;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Transports;

public sealed class TransportByLocationSpec : Specification<Transport>
{
    public TransportByLocationSpec(TransportSearch search)
    {
        if (!Enum.TryParse<TransportType>(search.TransportType, true, out var type))
            throw new IncorrectTransportTypeException();

        _ = type switch
        {
            TransportType.All => Query,
            _ => Query.Where(x => type.Equals(x.TransportType))
        };

        Query.Where(x => x.CanBeRented);
    }
}