using Ardalis.Specification;
using Simbir.GO.Server.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public class RentsByAccountSpec : Specification<Rent>
{
    public RentsByAccountSpec(long accountId) =>
        Query.Where(x => x.AccountId == accountId);
}