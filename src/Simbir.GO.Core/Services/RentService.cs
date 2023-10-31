using System.ComponentModel;
using Simbir.GO.Server.ApplicationCore.Contracts.Rents;
using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Specifications.Rents;
using Simbir.GO.Domain.Accounts.Errors;
using Simbir.GO.Domain.Rents;
using Simbir.GO.Domain.Rents.Enums;
using Simbir.GO.Domain.Rents.Errors;

namespace Simbir.GO.Server.ApplicationCore.Services;

public class RentService : IRentService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Rent> _rentRepository;
    private readonly ITransportService _transportService;

    public RentService(IAuthenticationService authenticationService, IRepository<Rent> rentRepository, ITransportService transportService)
    {
        _authenticationService = authenticationService;
        _rentRepository = rentRepository;
        _transportService = transportService;
  
    }

    public async Task<Rent> GetByIdAsync(long id)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
        if (await _rentRepository.GetByIdAsync(id) is not { } rent)
            throw new NotFoundRentException();
        
        var transport = await _transportService.GetByIdAsync(rent.TransportId);

        if (account.Id != rent.AccountId || account.Id != transport.TransportOwnerId) 
            throw new AccessDeniedException();

        return rent;
    }

    public async Task<Rent> StartAsync(long transportId, StartRentParams @params)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
        var transport = await _transportService.GetByIdAsync(transportId);
        
        if (!Enum.TryParse<PriceType>(@params.PriceType, true, out var type))
            throw new IncorrectPriceTypeException();
        
        if (account.Id == transport.TransportOwnerId)
            throw new InvalidCredentialsRentException();

        var price = type switch
        {
            PriceType.Minutes => transport.MinutePrice,
            PriceType.Days => transport.DayPrice,
            PriceType.None => throw new InvalidEnumArgumentException(),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (price == 0)
            throw new InvalidCredentialsRentException();

        var rent = Rent.Start(transport.Id, account.Id, type, price.Value);
        
        await _rentRepository.AddAsync(rent);
        await _transportService.StartRent(transport);
        

        return rent;
    }
    
    public async Task<Rent> EndAsync(long rentId, EndRentParams @params)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
        if (await _rentRepository.GetByIdAsync(rentId) is not { } rent)
            throw new NotFoundRentException();

        if (rent.AccountId != account.Id)
            throw new AccessDeniedException();

        if (rent.TimeEnd.HasValue)
            throw new InvalidCredentialsRentException();
        
        var transport = await _transportService.GetByIdAsync(rent.TransportId);
        
        if (account.Id != rent.AccountId)
            throw new InvalidCredentialsRentException();
        
        var endedRent = rent.End();

        await _transportService.EndRent(transport, @params.Latitude, @params.Longitude);
        await _rentRepository.UpdateAsync(endedRent);

        return endedRent;
    }

    public async Task<List<Rent>> GetAccountRentsAsync()
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
        var rents = await _rentRepository.ListAsync(new RentsByAccountSpec(account.Id));

        return rents;
    }
    
    public async Task<List<Rent>> GetTransportRentsAsync(long transportId)
    {
        var account = await _authenticationService.GetCurrentUserAsync();
        
        var transport = await _transportService.GetByIdAsync(transportId);
        
        if (account.Id != transport.TransportOwnerId)
            throw new InvalidCredentialsRentException();
        
        var rents = await _rentRepository.ListAsync(new RentsByTransportSpec(transportId));

        return rents;
    }

}