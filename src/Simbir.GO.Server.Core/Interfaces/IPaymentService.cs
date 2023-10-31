

namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface IPaymentService
{
    Task UpdateBalanceAsync(long accountId, double amount = 250_000);
}
