using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Transports;
using Simbir.GO.Server.ApplicationCore.Specifications.Transports;
using Simbir.GO.Server.Domain.Transports;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Admin;

public interface IAdminTransportService
{
    Task<List<Transport>> GetByFiltersAsync(TransportFilter filter);
    Task<Transport> GetByIdAsync(long id);
    Task<Transport> CreateAsync(AdminCreateTransportRequest request);
    Task<Transport> UpdateAsync(long id, AdminUpdateTransportRequest request);
    Task DeleteAsync(long id);
}