using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Transports;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Specifications.Transports;
using Simbir.GO.Domain.Accounts.Errors;
using Simbir.GO.Domain.Transports;
using Simbir.GO.Domain.Transports.Enums;
using Simbir.GO.Domain.Transports.Errors;
using Simbir.GO.Domain.Transports.ValueObjects;

namespace Simbir.GO.Server.ApplicationCore.Services.Admin;



public class AdminTransportService : IAdminTransportService
{

    private readonly IRepository<Transport> _transportRepository;
    private readonly IAdminAccountService _adminAccountService;

    public AdminTransportService(IRepository<Transport> transportRepository, IAdminAccountService adminAccountService)
    {
        _transportRepository = transportRepository;
        _adminAccountService = adminAccountService;
    }

    public async Task<List<Transport>> GetByFiltersAsync(TransportFilter filter)
    {
        var spec = new TransportSpec(filter);
        var workouts = await _transportRepository.ListAsync(spec);

        return workouts;
    }
    
    public async Task<Transport> GetByIdAsync(long id)
    {
        if (await _transportRepository.GetByIdAsync(id) is not { } transport)
            throw new NotFoundTransportException();

        return transport;
    }
    
    public async Task<Transport> CreateAsync(AdminCreateTransportRequest request)
    {
        var account = await _adminAccountService.GetByIdAsync(request.OwnerId);
        
        if (!Enum.TryParse<TransportType>(request.TransportType, true, out var type))
            throw new IncorrectTransportTypeException();

        var transport = Transport.Create(
            account.Id,
            type,
            request.Model,
            request.Color,
            request.Identifier,
            request.Description,
            Location.Create(request.Latitude, request.Longitude),
            request.MinutePrice,
            request.DayPrice,
            request.CanBeRented
        );

        await _transportRepository.AddAsync(transport);

        return transport;
    }
    
    public async Task<Transport> UpdateAsync(long id, AdminUpdateTransportRequest request)
    {
        var account = await _adminAccountService.GetByIdAsync(request.OwnerId);
        
        if (await _transportRepository.GetByIdAsync(id) is not { } transport)
            throw new NotFoundTransportException();
                
        if (!Enum.TryParse<TransportType>(request.TransportType, true, out var type))
            throw new IncorrectTransportTypeException();
        
        var updatedTransport = transport.Update(
            account.Id,
            type,
            request.Model,
            request.Color,
            request.Identifier,
            request.Description,
            Location.Create(request.Latitude, request.Longitude), 
            request.MinutePrice,
            request.DayPrice,
            request.CanBeRented
        );

        await _transportRepository.UpdateAsync(updatedTransport);

        return updatedTransport;
    }
    
    public async Task DeleteAsync(long id)
    {
        if (await _transportRepository.GetByIdAsync(id) is not { } transport)
            throw new NotFoundAccountException();
        
        await _transportRepository.DeleteAsync(transport);
    }
    
    public async Task StartRent(Transport transport) =>
        await _transportRepository.UpdateAsync(transport.RentStart());
    
    public async Task EndRent(Transport transport, double latitude, double longitude) =>
        await _transportRepository.UpdateAsync(transport.RentEnd(latitude, longitude));

}
