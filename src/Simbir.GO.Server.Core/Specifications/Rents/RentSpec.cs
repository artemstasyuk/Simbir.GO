using Ardalis.Specification;
using Simbir.GO.Server.ApplicationCore.Specifications.Helpers;
using Simbir.GO.Server.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public sealed class RentSpec: Specification<Rent>
{
    public RentSpec(RentFilter filter)
    {
        Query.OrderBy(x => x.Id);
        
        if (filter.IsPagingEnabled)
        {
            Query.Skip(PaginationHelper.CalculateSkip(filter))
                .Take(PaginationHelper.CalculateTake(filter));
        }
    }
}