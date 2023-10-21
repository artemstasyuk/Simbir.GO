
using Simbir.GO.Server.Domain.Enums;

namespace Simbir.GO.Server.Domain;

public sealed class Account 
{
    public Guid Id { get; private set; }
    
    public string Username { get; private set; }
    
    public string Password { get; private set; }
    
    public Role Role { get; private set;}

    public DateTime CreatedDateTime { get;}

    public DateTime UpdatedDateTime { get; private set;}

    public Account(Guid id, string username, string password, 
        Role role, DateTime createdDateTime, DateTime updatedDateTime)
    {
        Id = id;
        Username = username; 
        Password = password;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        Role = role;
    }

    
    public static Account Create(string username, string password, Role role) =>
        new(Guid.NewGuid(), username, password, role, DateTime.UtcNow, DateTime.UtcNow);

    
    public Account Update(string username, string password)
    {
        Username = username;
        Password = password;

        return this;
    }
}
