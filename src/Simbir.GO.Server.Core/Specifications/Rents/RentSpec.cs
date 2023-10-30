using Ardalis.Specification;
using Simbir.GO.Server.ApplicationCore.Specifications.Base;
using Simbir.GO.Server.ApplicationCore.Specifications.Helpers;
using Simbir.GO.Server.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public sealed class RentSpec: Specification<Rent>
{
    public RentSpec(RentFilter filter)
    {
        Query.OrderBy(x => x.Id);

        if (filter.LoadChildren)
            Query.Include(x => x.Transport);
        
        if (filter.IsPagingEnabled)
        {
            if (filter.Start != 0 && filter.Count != 0)
            {
                ((BaseFilter)filter).Start = filter.Start;
                ((BaseFilter)filter).Count = filter.Count;
            }
            Query.Skip(PaginationHelper.CalculateSkip(filter))
                .Take(PaginationHelper.CalculateTake(filter));
        }
    }
}