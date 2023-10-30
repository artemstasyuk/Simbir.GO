

namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface IPaymentService
{
    Task UpdateBalanceAsync(long accountId, double amount = 250);
    
    Task AddBalance(double amount = 250);
}
