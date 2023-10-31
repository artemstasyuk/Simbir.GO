using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Rents;
using Simbir.GO.Domain.Rents;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Admin;

public interface IAdminRentService
{
    Task<Rent> GetByIdAsync(long id);
    
    Task<List<Rent>> GetAccountRentsAsync(long userId);
    
    Task<List<Rent>> GetTransportRentsAsync(long transportId);
    
    Task<Rent> EndAsync(long rentId, AdminEndRentRequest request);
    
    Task<Rent> CreateAsync(AdminCreateRentRequest request);
    
    Task<Rent> UpdateAsync(long id, AdminUpdateRentRequest request);
    
    Task DeleteAsync(long rentId);
}