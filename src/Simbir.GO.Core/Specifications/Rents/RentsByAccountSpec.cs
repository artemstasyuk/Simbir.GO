using Ardalis.Specification;
using Simbir.GO.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public sealed class RentsByAccountSpec : Specification<Rent>
{
    public RentsByAccountSpec(long accountId) =>
        Query.Where(x => x.AccountId == accountId);
}