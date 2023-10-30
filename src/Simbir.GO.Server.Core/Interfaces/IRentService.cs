using Simbir.GO.Server.ApplicationCore.Contracts.Rents;
using Simbir.GO.Server.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface IRentService
{
    Task<Rent> GetByIdAsync(long id);
    
    Task<Rent> StartAsync(long transportId, StartRentRequest request);
    
    Task<Rent> EndAsync(long rentId, EndRentRequest request);

    Task<List<Rent>> GetAccountRentsAsync();

    Task<List<Rent>> GetTransportRentsAsync(long transportId);

}