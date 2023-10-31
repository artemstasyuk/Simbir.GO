using Ardalis.Specification;
using Simbir.GO.Server.ApplicationCore.Specifications.Helpers;
using Simbir.GO.Server.Domain.Transports;
using Simbir.GO.Server.Domain.Transports.Enums;
using Simbir.GO.Server.Domain.Transports.Errors;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Transports;

public sealed class TransportSpec: Specification<Transport>
{
    public TransportSpec(TransportFilter filter)
    {
        Query.OrderBy(x => x.Id);

        if (filter.LoadChildren)
            Query.Include(x => x.TransportRents);

        
        if (!string.IsNullOrEmpty(filter.TransportType))
        {
            if (!Enum.TryParse<TransportType>(filter.TransportType, true, out var type))
                throw new IncorrectTransportTypeException();
            Query.Where(x => x.TransportType == type);
        }
        
        if (filter.IsPagingEnabled)
        {
            Query.Skip(PaginationHelper.CalculateSkip(filter))
                .Take(PaginationHelper.CalculateTake(filter));
        }
    }
}