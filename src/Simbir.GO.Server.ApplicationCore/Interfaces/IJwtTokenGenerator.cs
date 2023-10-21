using Simbir.GO.Server.Domain;

namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Account account);
}