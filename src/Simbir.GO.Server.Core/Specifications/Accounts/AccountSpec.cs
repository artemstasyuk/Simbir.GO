using Ardalis.Specification;
using Simbir.GO.Server.ApplicationCore.Specifications.Base;
using Simbir.GO.Server.ApplicationCore.Specifications.Helpers;
using Simbir.GO.Server.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Accounts;

public sealed class AccountSpec: Specification<Account>
{
    public AccountSpec(AccountFilter filter)
    {
        Query.OrderBy(x => x.Username)
            .ThenByDescending(x => x.Id);

        if (filter.LoadChildren)
            Query.Include(x => x.AccountRents)
                .Include(x => x.AccountTransport);

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