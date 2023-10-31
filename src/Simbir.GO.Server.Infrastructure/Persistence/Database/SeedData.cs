using Microsoft.EntityFrameworkCore;
using Serilog;
using Simbir.GO.Server.ApplicationCore.Constants;
using Simbir.GO.Server.ApplicationCore.Security;
using Simbir.GO.Server.Domain.Accounts;
using Simbir.GO.Server.Domain.Accounts.Enums;
using Simbir.GO.Server.Domain.Rents;
using Simbir.GO.Server.Domain.Rents.Enums;
using Simbir.GO.Server.Domain.Transports;
using Simbir.GO.Server.Domain.Transports.Enums;
using Simbir.GO.Server.Domain.Transports.ValueObjects;

namespace Simbir.GO.Server.Infrastructure.Persistence.Database;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext context, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (context.Database.IsNpgsql())
            {
                await context.Database.MigrateAsync();
            }
            if (!await context.Accounts.AnyAsync())
            {
                await context.Accounts.AddRangeAsync(GetPreconfiguredAccounts());
                await context.SaveChangesAsync();
            }

            if (!await context.Transports.AnyAsync())
            {
                await context.Transports.AddRangeAsync(GetPreconfiguredTransports());
                await context.SaveChangesAsync();
            }
            
            if (!await context.Rents.AnyAsync())
            {
                await context.Rents.AddRangeAsync(GetPreconfiguredRents());
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            Log.Error(ex.Message);
            await SeedAsync(context, retryForAvailability);
            throw;
        }


        static List<Account> GetPreconfiguredAccounts()
        {
            var adminSalt = Encryption.CreateSaltKey(SecurityConstants.PasswordSaltKeySize);
            var adminHash =
                Encryption.CreatePasswordHash("1234", adminSalt, SecurityConstants.DefaultHashedPasswordFormat);

            var customerSalt = Encryption.CreateSaltKey(SecurityConstants.PasswordSaltKeySize);
            var customerHash = Encryption.CreatePasswordHash("1234", customerSalt,
                SecurityConstants.DefaultHashedPasswordFormat);

            List<Account> accounts = new List<Account>()
            {
                Account.Create("admin", adminHash, adminSalt, Role.Admin),
                Account.Create("customer", customerHash, customerSalt, Role.Customer),
            };

            return accounts;
        }

        static IEnumerable<Transport> GetPreconfiguredTransports()
        {
            return new List<Transport>
            {
                Transport.Create(1, TransportType.Car, "Model1", "Green", "15k71c",
                    "Green car", Location.Create(25, 51), 51, 52, true),
                Transport.Create(1, TransportType.Bike, "Model2", "Red", "22234c",
                    "Red bike", Location.Create(80, 80), 12, 0, true),
                Transport.Create(1, TransportType.Car, "Model3", "Blue", "5k71c",
                    "Blue car", Location.Create(25, 51), 51, 52, true),
                Transport.Create(2, TransportType.Bike, "Model4", "Yellow", "7689bc",
                    "Yellow bike", Location.Create(40, 60), 10, 0, true),
                Transport.Create(2, TransportType.Car, "Model5", "Black", "31h78d",
                    "Black car", Location.Create(10, 30), 30, 35, true),
                Transport.Create(1, TransportType.Bike, "Model6", "White", "9867dj",
                    "White bike", Location.Create(70, 45), 8, 0, true),
                Transport.Create(2, TransportType.Car, "Model7", "Silver", "35g92k",
                    "Silver car", Location.Create(15, 55), 55, 60, true),
                Transport.Create(1, TransportType.Bike, "Model8", "Blue", "16753j",
                    "Blue bike", Location.Create(90, 20), 10, 0, true),
                Transport.Create(2, TransportType.Car, "Model9", "Red", "9g8k4d",
                    "Red car", Location.Create(45, 75), 75, 80, true),
                Transport.Create(1, TransportType.Scooter, "Model10", "Green", "678dj4",
                    "Green scooter", Location.Create(50, 40), 6, 0, true),
            };
        }
        static IEnumerable<Rent> GetPreconfiguredRents()
        {
            return new List<Rent>
            {
                Rent.Create(7,  1, PriceType.Minutes, 55, DateTime.UtcNow, null, null),
                Rent.Create(1,  2, PriceType.Days, 52, DateTime.UtcNow, null, null),
            };
        }
    }
}
