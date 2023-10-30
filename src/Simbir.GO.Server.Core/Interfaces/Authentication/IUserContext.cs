using Simbir.GO.Server.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;

public interface IUserContext
{
    Task<Account?> GetCurrentUserAsync();
}