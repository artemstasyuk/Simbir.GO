using Simbir.GO.Server.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Account account);
}