using Simbir.GO.Server.ApplicationCore.Constants;
using Simbir.GO.Server.ApplicationCore.Security;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Domain.Transports;
using Simbir.GO.Server.Domain.Transports.Enums;
using Simbir.GO.Server.Domain.Transports.ValueObjects;

namespace Simbir.GO.Server.Infrastructure.Persistence.Database;

public static class SeedData
{
    public static List<Account> CreateUsers()
    {
        var adminSalt = Encryption.CreateSaltKey (SecurityConstants.PasswordSaltKeySize);
        var adminHash = Encryption.CreatePasswordHash("1234", adminSalt, SecurityConstants.DefaultHashedPasswordFormat);
        
        var customerSalt = Encryption.CreateSaltKey (SecurityConstants.PasswordSaltKeySize);
        var customerHash = Encryption.CreatePasswordHash("1234", customerSalt, SecurityConstants.DefaultHashedPasswordFormat);

        List<Account> accounts = new List<Account>()
        {
            Account.Create("admin", adminHash, adminSalt, Role.Admin),
            Account.Create("customer", customerHash, customerSalt, Role.Customer)
        };

        return accounts;
    }
    
    public static Transport CreateTransport()
    {
        return Transport.Create(1, TransportType.Car, "Model1", "Green", "5k71c",
            "Green car", Location.Create(25, 51), 51, 52, true);
    }
}