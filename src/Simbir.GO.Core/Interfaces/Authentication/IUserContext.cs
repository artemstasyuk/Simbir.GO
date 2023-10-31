using Simbir.GO.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;

public interface IUserContext
{
    Task<Account?> GetCurrentUserAsync();
    string GetToken();
}