using Simbir.GO.Server.ApplicationCore.Contracts.Authentication;
using Simbir.GO.Server.Domain.Accounts;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;

/// <summary>
/// Authentication service interface
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Sign in
    /// </summary>
    /// <param name="request">Login request</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task<AuthResult> SignInAsync(SignInRequest request);
    
    /// <summary>
    /// Sign up
    /// </summary>
    /// <param name="request">Register request</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task<AuthResult> SignUpAsync(SignUpRequest request);

    /// <summary> 
    /// Sign out
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task SignOutAsync();

    /// <summary>
    /// Get authenticated user
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the account
    /// </returns>
    Task<Account> GetCurrentUserAsync();
}