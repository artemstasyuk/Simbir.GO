using System.ComponentModel;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Rents;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Specifications.Rents;
using Simbir.GO.Server.Domain.Accounts.Errors;
using Simbir.GO.Server.Domain.Rents;
using Simbir.GO.Server.Domain.Rents.Enums;
using Simbir.GO.Server.Domain.Rents.Errors;
using Simbir.GO.Server.Domain.Transports.Errors;

namespace Simbir.GO.Server.ApplicationCore.Services.Admin;


public class AdminRentService : IAdminRentService
{
    private readonly IRepository<Rent> _rentRepository;
    private readonly IAdminAccountService _adminAccountService;
    private readonly IAdminTransportService _transportService;

    public AdminRentService(IRepository<Rent> rentRepository, IAdminAccountService adminAccountService, IAdminTransportService transportService)
    {
        _rentRepository = rentRepository;
        _adminAccountService = adminAccountService;
        _transportService = transportService;
    }

    public async Task<Rent> GetByIdAsync(long id)
    {
        if (await _rentRepository.GetByIdAsync(id) is not { } rent)
            throw new NotFoundRentException();

        return rent;
    }

    public async Task<List<Rent>> GetAccountRentsAsync(long userId)
    {
        if (await _adminAccountService.GetByIdAsync(userId) is not { } account)
            throw new NotFoundAccountException();

        var rents = await _rentRepository.ListAsync(new RentsByAccountSpec(account.Id));

        return rents;
    }
    
    public async Task<List<Rent>> GetTransportRentsAsync(long transportId)
    {
        if (await _transportService.GetByIdAsync(transportId) is not { } transport)
            throw new NotFoundTransportException();

        var rents = await _rentRepository.ListAsync(new RentsByTransportSpec(transport.Id));

        return rents;
    }
    
    public async Task<Rent> EndAsync(long rentId, AdminEndRentRequest request)
    {
        var rent = await GetByIdAsync(rentId);
        
        var transport = await _transportService.GetByIdAsync(rent.TransportId);
        
        rent.End();
        transport.Location.Update(request.Latitude, request.Longitude);
        
        await _rentRepository.UpdateAsync(rent);

        return rent;
    }
    
    public async Task<Rent> CreateAsync(AdminCreateRentRequest request)
    {
        var transport = await _transportService.GetByIdAsync(request.TransportId);
        
        var account = await _adminAccountService.GetByIdAsync(request.UserId);
        
        if (!Enum.TryParse<PriceType>(request.PriceType, true, out var type))
            throw new IncorrectPriceTypeException();
        
        if (!DateTime.TryParse(request.TimeStart, out var timeStart))
            throw new InvalidCredentialsRentException();
        
        if (!DateTime.TryParse(request.TimeEnd, out var timeEnd))
            throw new InvalidCredentialsRentException();
        
        var rent = Rent.Create(
            transport.Id, 
            account.Id,
            type, 
            request.PriceOfUnit,
            timeStart,
            timeEnd,
            request.FinalPrice
        );

        await _rentRepository.AddAsync(rent);

        return rent;
    }
    
    public async Task<Rent> UpdateAsync(long id, AdminUpdateRentRequest request)
    {
        var rent = await GetByIdAsync(id);
        
        var transport = await _transportService.GetByIdAsync(request.TransportId);
        
        var account = await _adminAccountService.GetByIdAsync(request.UserId);
        
        if (!Enum.TryParse<PriceType>(request.PriceType, true, out var type))
            throw new IncorrectPriceTypeException();
        
        if (!DateTime.TryParse(request.TimeStart, out var timeStart))
            throw new InvalidCredentialsRentException();
        
        if (!DateTime.TryParse(request.TimeEnd, out var timeEnd))
            throw new InvalidCredentialsRentException();
        
        var price = type switch
        {
            PriceType.Minutes => transport.MinutePrice,
            PriceType.Days => transport.DayPrice,
            PriceType.None => throw new InvalidEnumArgumentException(),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (price == null)
            throw new InvalidCredentialsRentException();
        
        rent.Update(
            transport.Id, 
            account.Id,
            type, 
            price.Value,
            timeStart, 
            timeEnd,
            request.FinalPrice
        );

        await _rentRepository.UpdateAsync(rent);

        return rent;
    }
    
    public async Task DeleteAsync(long rentId)
    {
        if (await _rentRepository.GetByIdAsync(rentId) is not { } rent)
            throw new NotFoundRentException();
        
        await _rentRepository.DeleteAsync(rent);
    }
}
    
    