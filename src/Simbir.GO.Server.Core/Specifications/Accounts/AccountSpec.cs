using Ardalis.Specification;
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
                .Include(x => x.AccountTransports);
           

        if (filter.IsPagingEnabled)
        {
            Query.Skip(PaginationHelper.CalculateSkip(filter))
                .Take(PaginationHelper.CalculateTake(filter));
        }
    }
}