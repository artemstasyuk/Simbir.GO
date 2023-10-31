using Simbir.GO.Server.ApplicationCore.Contracts.Rents;
using Simbir.GO.Server.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface IRentService
{
    Task<Rent> GetByIdAsync(long id);

    Task<Rent> StartAsync(long transportId, StartRentParams @params);
    
    Task<Rent> EndAsync(long rentId, EndRentParams @params);

    Task<List<Rent>> GetAccountRentsAsync();

    Task<List<Rent>> GetTransportRentsAsync(long transportId);

}