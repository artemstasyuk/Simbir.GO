using Simbir.GO.Server.ApplicationCore.Contracts.Accounts;
using Simbir.GO.Server.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface IAccountService
{
    Task<Account> GetByIdAsync(long id);
    Task<Account> EditAsync(EditAccountRequest request);
}