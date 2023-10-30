using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Server.Domain.Transports;

namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface ITransportService
{
    Task<Transport> GetByIdAsync(long transportId);
    
    Task<Transport> CreateAsync(CreateTransportRequest request);

    Task<Transport> UpdateAsync(long transportId, UpdateTransportRequest request);

    Task<List<Transport>> GetListByLocationAsync(TransportSearch search);
    
    Task DeleteAsync(long transportId);
}