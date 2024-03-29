using Simbir.GO.Domain.Accounts;
using Simbir.GO.Domain.Accounts.Enums;
using Simbir.GO.Domain.Accounts.Errors;
using Simbir.GO.Server.ApplicationCore.Auth;
using Simbir.GO.Server.ApplicationCore.Constants;
using Simbir.GO.Server.ApplicationCore.Contracts.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Security;
using Simbir.GO.Server.ApplicationCore.Specifications.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Services;

/// <summary>
/// Implements <see cref="IAuthenticationService"/>.
/// </summary>
public class AuthenticationService : IAuthenticationService
{

    private readonly ITokenizer _tokenizer;
    private readonly IRepository<Account> _accountRepository;
    private readonly IRepository<RevokedToken> _revokedTokenRepository;
    private readonly IUserContext _userContext;
   

    public AuthenticationService(ITokenizer tokenizer, IRepository<Account> accountRepository, IUserContext userContext, IRepository<RevokedToken> revokedTokenRepository)
    {
        _tokenizer = tokenizer;
        _accountRepository = accountRepository;
        _userContext = userContext;
        _revokedTokenRepository = revokedTokenRepository;
    }


    /// <summary>
    /// Sign in
    /// </summary>
    /// <param name="request">SignIn request</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task<AuthResult> SignInAsync(SignInRequest request)
    {
        //validate
        if (await _accountRepository.FirstOrDefaultAsync(new AccountByUsernameSpec(request.Username)) is not { } account)
            throw  new InvalidCredentialsAccountException();
        
        if (!PasswordsMatch(account.PasswordHash, account.PasswordSalt, request.Password))
            throw new InvalidCredentialsAccountException();
        
        var token = _tokenizer.GenerateToken(account);

        return new AuthResult(account.Username, token);
    }
    

    /// <summary>
    /// Sign up
    /// </summary>
    /// <param name="request">SignUp request</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task<AuthResult> SignUpAsync(SignUpRequest request)
    {
        //validate 
        
        if (await _accountRepository.FirstOrDefaultAsync(new AccountByUsernameSpec(request.Username)) is not null)
            throw new DuplicateUsernameAccountException();

        var salt = Encryption.CreateSaltKey(SecurityConstants.PasswordSaltKeySize);

        var account = Account.Create(
            request.Username,
            Encryption.CreatePasswordHash(request.Password, salt, SecurityConstants.DefaultHashedPasswordFormat),
            salt,
            Role.Customer
        );

        await _accountRepository.AddAsync(account);
            
        var token = _tokenizer.GenerateToken(account);
        
        return new AuthResult(account.Username, token);
    }


    /// <summary> 
    /// Sign out
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task SignOutAsync()
    {
        var token = _userContext.GetToken();
        var securityToken = _tokenizer.ParseToken(token);
        var user = await GetCurrentUserAsync();

        var revokedToken = new RevokedToken(
            token,
            securityToken.Id, 
            user.Id, 
            true,
            DateTime.UtcNow);

        await _revokedTokenRepository.AddAsync(revokedToken);
    }
    
   /// <summary>
   /// Get authenticated account
   /// </summary>
   /// <returns>
   /// A task that represents the asynchronous operation
   /// The task result contains the account
   /// </returns>
   public async Task<Account> GetCurrentUserAsync()
   {
       if (await _userContext.GetCurrentUserAsync() is not { } account)
           throw new NotFoundAccountException();

       return account;
   }
    

    #region Helpers

    /// <summary>
    /// Check whether the entered password matches with a saved one
    /// </summary>
    /// <param name="userPassword">The user password</param>
    /// <param name="enteredPassword">The entered password</param>
    /// <param name="salt">The salt password</param>
    /// <returns>True if passwords match; otherwise false</returns>
    private bool PasswordsMatch(string userPassword,string salt, string enteredPassword)
    {
        if (userPassword == null || string.IsNullOrEmpty(enteredPassword))
            return false;
        
        var savedPassword = Encryption.CreatePasswordHash(
            enteredPassword,
            salt, 
            SecurityConstants.DefaultHashedPasswordFormat);
        
        return userPassword.Equals(savedPassword);
    }

    #endregion
}
