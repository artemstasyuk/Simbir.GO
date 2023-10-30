using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Accounts;
using Simbir.GO.Server.ApplicationCore.Specifications.Accounts;
using Simbir.GO.Server.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Admin;

public interface IAdminAccountService
{
    Task<Account> GetByIdAsync(long id);
    Task<List<Account>> GetByFiltersAsync(AccountFilter filter);
    Task<Account> CreateAsync(AdminCreateAccountRequest request);
    Task<Account> UpdateAsync(long id, AdminUpdateAccountRequest request);
    Task DeleteAsync(long id);
}