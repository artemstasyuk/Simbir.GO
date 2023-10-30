using Simbir.GO.Server.ApplicationCore.Constants;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Accounts;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Security;
using Simbir.GO.Server.ApplicationCore.Specifications.Accounts;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Domain.Accounts.Errors;

namespace Simbir.GO.Server.ApplicationCore.Services.Admin;


public class AdminAccountService : IAdminAccountService
{
    private readonly IRepository<Account> _accountRepository;

    public AdminAccountService(IRepository<Account> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> GetByIdAsync(long id)
    {
        if (await _accountRepository.GetByIdAsync(id) is not { } account)
            throw new NotFoundAccountException();

        return account;
    }
    
    
    public async Task<List<Account>> GetByFiltersAsync(AccountFilter filter)
    {
        var spec = new AccountSpec(filter);
        var workouts = await _accountRepository.ListAsync(spec);

        return workouts;
    }

    public async Task<Account> CreateAsync(AdminCreateAccountRequest request)
    {
        if (await _accountRepository.FirstOrDefaultAsync(new AccountByUsernameSpec(request.Username)) is not null)
            throw new DuplicateUsernameAccountException();

        var role = Role.Customer;
        if (request.IsAdmin)
            role = Role.Admin;
        
        var salt = Encryption.CreateSaltKey(SecurityConstants.PasswordSaltKeySize);
        var passwordHash = Encryption.CreatePasswordHash(request.Password, salt, SecurityConstants.DefaultHashedPasswordFormat);

        var account = Account.Create(request.Username, passwordHash, salt, role, request.Balance);

        await _accountRepository.AddAsync(account);

        return account;
    }
    
    public async Task<Account> UpdateAsync(long id, AdminUpdateAccountRequest request)
    {
        if (await _accountRepository.GetByIdAsync(id) is not { } account)
            throw new NotFoundAccountException();
        
        if (await _accountRepository.FirstOrDefaultAsync(new AccountByUsernameSpec(request.Username)) is not null)
            throw new DuplicateUsernameAccountException();
        
        var role = Role.Customer;
        if (request.IsAdmin)
            role = Role.Admin;
        
        var salt = Encryption.CreateSaltKey(SecurityConstants.PasswordSaltKeySize);
        var passwordHash = Encryption.CreatePasswordHash(request.Password, salt, SecurityConstants.DefaultHashedPasswordFormat);

        account.Update(request.Username, passwordHash, salt, role);

        await _accountRepository.UpdateAsync(account);

        return account;
    }
    
    public async Task DeleteAsync(long id)
    {
        if (await _accountRepository.GetByIdAsync(id) is not { } account)
            throw new NotFoundAccountException();
        
        await _accountRepository.DeleteAsync(account);
    }
}
