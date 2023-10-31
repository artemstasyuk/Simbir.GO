using Simbir.GO.Domain.Accounts;
using Simbir.GO.Domain.Common.Entity;

namespace Simbir.GO.Server.ApplicationCore.Auth;

public class RevokedToken : AggregateRoot
{
    public string Token { get;  set; }
    public string JwtId { get;  set; } 
    public long AccountId { get;  set; }
    public bool IsRevoked { get;  set; }
    public DateTime CreatedDateTime { get;  set; }
    
    public RevokedToken(string token, string jwtId, long accountId, bool isRevoked, DateTime createdDateTime)
    {
        Token = token;
        JwtId = jwtId;
        AccountId = accountId;
        IsRevoked = isRevoked;
        CreatedDateTime = createdDateTime;
    }
    
    public Account? Account;
}
