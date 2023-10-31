using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Simbir.GO.Domain.Accounts;
using Simbir.GO.Infrastructure.Persistence;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Helpers;

namespace Simbir.GO.Infrastructure.Auth;

public class Tokenizer : ITokenizer
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly AppDbContext _context;

    public Tokenizer(IOptions<JwtSettings> jwtSettings, IDateTimeProvider dateTimeProvider, AppDbContext context)
    {
        _jwtSettings = jwtSettings.Value;
        _dateTimeProvider = dateTimeProvider;
        _context = context;
    }

    public string GenerateToken(Account account)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
    
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, account.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, account.Role.ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims, 
            signingCredentials: signingCredentials);

        
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }


    public JwtSecurityToken ParseToken(string tokenStr)
    {
        var jwtToken = new JwtSecurityToken(tokenStr);
        return jwtToken;
    }

    public bool IsValid(string tokenStr)
    {
        var jwtToken = new JwtSecurityToken(tokenStr);
        var revokedToken = _context.RevokedTokens
            .FirstOrDefault(token => token.IsRevoked && token.JwtId == jwtToken.Id);
            return revokedToken is null;
    }
}
