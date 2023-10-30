using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Services;

public class PaymentService : IPaymentService
{
    private readonly IAccountService _accountService;
    private readonly IRepository<Account> _accountRepository;
    private readonly IAuthenticationService _authenticationService;

    public PaymentService(IAccountService accountService, IAuthenticationService authenticationService, IRepository<Account> accountRepository)
    {
        _accountService = accountService;
        _authenticationService = authenticationService;
        _accountRepository = accountRepository;
    }
    
    public async Task UpdateBalanceAsync(long accountId, double amount = 250)
    {
        var account = await _accountService.GetByIdAsync(accountId);

        account.UpdateBalance(amount);

        await _accountRepository.UpdateAsync(account);
    }

    public async Task AddBalance(double amount = 250)
    {
        var account = await _authenticationService.GetCurrentUserAsync();

        await _accountRepository.UpdateAsync(account);
        
        account.UpdateBalance(amount);
    }
}