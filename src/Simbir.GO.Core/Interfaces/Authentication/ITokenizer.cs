using System.IdentityModel.Tokens.Jwt;
using Simbir.GO.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;

public interface ITokenizer
{
    string GenerateToken(Account account);

    JwtSecurityToken ParseToken(string token);

    bool IsValid(string accessToken);
}