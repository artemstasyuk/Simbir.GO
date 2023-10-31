using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Simbir.GO.Domain.Accounts;
using Simbir.GO.Domain.Accounts.Errors;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;

namespace Simbir.GO.Infrastructure.Auth;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<Account> _repository;

    public UserContext(IHttpContextAccessor httpContextAccessor, IRepository<Account> repository)
    {
        _httpContextAccessor = httpContextAccessor;
        _repository = repository;
    }

    public async Task<Account?> GetCurrentUserAsync()
    {
        if (!TryGetUserId(out var userId))
            return null;

        return await _repository.GetByIdAsync(userId);
    }

    private bool TryGetUserId(out long result)
    {
        var id = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return long.TryParse(id, out result);
    }

    public string GetToken()
    {
        var jwtToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("bearer ", string.Empty);
        return !string.IsNullOrEmpty(jwtToken) ? jwtToken : throw new UnauthorizedException();
    }
}