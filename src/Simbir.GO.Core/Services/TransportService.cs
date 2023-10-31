using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Specifications.Transports;
using Simbir.GO.Domain.Accounts.Errors;
using Simbir.GO.Domain.Transports;
using Simbir.GO.Domain.Transports.Enums;
using Simbir.GO.Domain.Transports.Errors;
using Simbir.GO.Domain.Transports.ValueObjects;

namespace Simbir.GO.Server.ApplicationCore.Services;

public class TransportService : ITransportService
{

    private readonly IRepository<Transport> _transportRepository;
    private readonly IUserContext _userContext;
    private readonly IAuthenticationService _authenticationService;
    

    public TransportService(IRepository<Transport> transportRepository, IUserContext userContext, IAuthenticationService authenticationService)
    {
        _transportRepository = transportRepository;
        _userContext = userContext;
        _authenticationService = authenticationService;
    }
    
    public async Task<Transport> GetByIdAsync(long id)
    {
        if (await _transportRepository.GetByIdAsync(id) is not { } transport)
            throw new NotFoundTransportException();

        return transport;
    }

    public async Task<List<Transport>> GetListByLocationAsync(TransportSearch search)
    {
        if (!Enum.TryParse<TransportType>(search.TransportType, true, out var type))
            throw new IncorrectTransportTypeException();

        var filteredList = await _transportRepository.ListAsync(new TransportByLocationSpec(search));
        
        var result = filteredList.Where(transport =>
            transport.Location.CalculateDistance(search.Latitude, search.Longitude, transport.Location) 
            <= search.Radius).ToList();
        
        
        return result;

    }
    
    public async Task<Transport> CreateAsync(CreateTransportRequest request)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
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

    public async Task<Transport> UpdateAsync(long transportId, UpdateTransportRequest request)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
        if (await _transportRepository.GetByIdAsync(transportId) is not { } transport)
            throw new NotFoundTransportException();

        if (account.Id != transport.TransportOwnerId)
            throw new AccessDeniedException();
                
        var updatedTransport = transport.Update(
            transport.TransportOwnerId,
            transport.TransportType,
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

    public async Task DeleteAsync(long transportId)
    {
        if (await _userContext.GetCurrentUserAsync() is not { } account)
         throw new NotFoundAccountException();
        
        if (await _transportRepository.GetByIdAsync(transportId) is not { } transport)
            throw new NotFoundTransportException();

        if (account.Id != transport.TransportOwnerId)
            throw new AccessDeniedException();
        
        await _transportRepository.DeleteAsync(transport);
    }
    
    public async Task StartRent(Transport transport) =>
        await _transportRepository.UpdateAsync(transport.RentStart());
    
    public async Task EndRent(Transport transport, double latitude, double longitude) =>
        await _transportRepository.UpdateAsync(transport.RentEnd(latitude, longitude));
}
