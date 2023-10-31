using Ardalis.Specification;
using Simbir.GO.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Accounts;

public sealed class AccountByUsernameSpec: Specification<Account>, ISingleResultSpecification<Account>
{
    public AccountByUsernameSpec(string name) =>
        Query.Where(x => x.Username == name);
}