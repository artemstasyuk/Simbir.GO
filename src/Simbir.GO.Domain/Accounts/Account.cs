using Simbir.GO.Domain.Accounts.Enums;
using Simbir.GO.Domain.Common.Entity;
using Simbir.GO.Domain.Rents;
using Simbir.GO.Domain.Transports;

namespace Simbir.GO.Domain.Accounts;

/// <summary>
/// Represents an account entity
/// </summary>
public sealed class Account : AggregateRoot
{
    
    /// <summary>
    /// Gets or sets the account Username
    /// </summary>
    public string Username { get; private set; }
    
    /// <summary>
    /// Gets or sets the account PasswordHash
    /// </summary>
    public string PasswordHash { get; private set; }
    
    /// <summary>
    /// Gets or sets the account PasswordSalt
    /// </summary>
    public string PasswordSalt { get; private set; }
    
    /// <summary>
    /// Gets or sets the account Role
    /// </summary>
    public Role Role { get; private set;}
    
    /// <summary>
    /// Gets or sets the account Balance
    /// </summary>
    public double Balance { get; private set; }
    
    /// <summary>
    /// Gets or sets the date and time of instance creation
    /// </summary>
    public DateTime CreatedDateTime { get;}

    /// <summary>
    /// Gets or sets the date and time of instance updating
    /// </summary>
    public DateTime UpdatedDateTime { get; private set;}
    
    /// <summary>
    /// Read only list of accounts rents
    /// </summary>
    public IReadOnlyList<Rent> AccountRents { get; set; }
    
    /// <summary>
    /// Read only list of accounts rents
    /// </summary>
    public IReadOnlyList<Transport> AccountTransports { get; set; }
    
    /// <summary>
    /// Ctor
    /// </summary>
    public Account(string username, string passwordHash, string passwordSalt, Role role, 
        double balance,  DateTime createdDateTime, DateTime updatedDateTime) 
    {
        Username = username; 
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Balance = balance;
        Role = role;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    } 
    
    public static Account Create(string username, string passwordHash, string passwordSalt, Role role, double balance = 0) =>
        new(username, passwordHash, passwordSalt, role, balance, DateTime.UtcNow, DateTime.UtcNow);
    
    public Account Update(string username, string passwordHash, string passwordSalt, double ballance,  Role role)
    {
        Username = username;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Balance = ballance;
        Role = role;

        UpdatedDateTime = DateTime.UtcNow;

        return this;
    }
    
    public Account Edit(string username, string passwordHash, string passwordSalt)
    {
        Username = username;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;

        UpdatedDateTime = DateTime.UtcNow;

        return this;
    }
    
    public Account UpdateBalance(double balance)
    {
        Balance += balance;
        
        return this;
    }
}
