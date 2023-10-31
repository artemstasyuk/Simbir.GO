using Simbir.GO.Server.ApplicationCore.Constants;
using Simbir.GO.Server.ApplicationCore.Contracts.Accounts;
using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Security;
using Simbir.GO.Server.ApplicationCore.Specifications.Accounts;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Accounts.Errors;

namespace Simbir.GO.Server.ApplicationCore.Services;

public class AccountService : IAccountService
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IAuthenticationService _authenticationService;

    public AccountService(IRepository<Account> accountRepository, IAuthenticationService authenticationService)
    {
        _accountRepository = accountRepository;
        _authenticationService = authenticationService;
    }


    public async Task<Account> GetByIdAsync(long id)
    {
        if (await _accountRepository.GetByIdAsync(id) is not { } account)
            throw new NotFoundAccountException();

        return account;
    }

    public async Task<Account> EditAsync(EditAccountRequest request)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
        if (await _accountRepository.FirstOrDefaultAsync(new AccountByUsernameSpec(request.Username)) is not null)
            throw new DuplicateUsernameAccountException();

        var salt = Encryption.CreateSaltKey(SecurityConstants.PasswordSaltKeySize);
        var passwordHash = Encryption.CreatePasswordHash(request.Password, salt, SecurityConstants.DefaultHashedPasswordFormat);

        account.Edit(request.Username, passwordHash, salt);

        await _accountRepository.UpdateAsync(account);

        return account;
    }
}