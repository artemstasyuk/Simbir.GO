using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Domain.Accounts.Errors;

namespace Simbir.GO.Server.ApplicationCore.Services;

public class PaymentService : IPaymentService
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IAuthenticationService _authenticationService;

    public PaymentService(IAuthenticationService authenticationService, IRepository<Account> accountRepository)
    {
        _authenticationService = authenticationService;
        _accountRepository = accountRepository;
    }
    
    public async Task UpdateBalanceAsync(long accountId, double amount = 250_000)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        if (accountId != account.Id && account.Role == Role.Customer)
            throw new AccessDeniedAccountException();
        
        account.UpdateBalance(amount);

        await _accountRepository.UpdateAsync(account);
    }
}
