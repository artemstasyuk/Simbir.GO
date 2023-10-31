using System.ComponentModel;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Rents;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;
using Simbir.GO.Server.ApplicationCore.Specifications.Rents;
using Simbir.GO.Domain.Accounts.Errors;
using Simbir.GO.Domain.Rents;
using Simbir.GO.Domain.Rents.Enums;
using Simbir.GO.Domain.Rents.Errors;
using Simbir.GO.Domain.Transports.Errors;

namespace Simbir.GO.Server.ApplicationCore.Services.Admin;


public class AdminRentService : IAdminRentService
{
    private readonly IRepository<Rent> _rentRepository;
    private readonly IAdminAccountService _adminAccountService;
    private readonly IAdminTransportService _adminTransportService;

    public AdminRentService(IRepository<Rent> rentRepository, IAdminAccountService adminAccountService, IAdminTransportService adminTransportService)
    {
        _rentRepository = rentRepository;
        _adminAccountService = adminAccountService;
        _adminTransportService = adminTransportService;
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
        if (await _adminTransportService.GetByIdAsync(transportId) is not { } transport)
            throw new NotFoundTransportException();

        var rents = await _rentRepository.ListAsync(new RentsByTransportSpec(transport.Id));

        return rents;
    }
    
    public async Task<Rent> EndAsync(long rentId, AdminEndRentRequest request)
    {
        var rent = await GetByIdAsync(rentId);
        
        var transport = await _adminTransportService.GetByIdAsync(rent.TransportId);
        
        var endedRent = rent.End();
        
        await _adminTransportService.EndRent(transport, request.Latitude, request.Longitude);
        await _rentRepository.UpdateAsync(endedRent);

        return rent;
    }
    
    public async Task<Rent> CreateAsync(AdminCreateRentRequest request)
    {
        var transport = await _adminTransportService.GetByIdAsync(request.TransportId);
        
        var account = await _adminAccountService.GetByIdAsync(request.UserId);
        
        if (!Enum.TryParse<PriceType>(request.PriceType, true, out var type))
            throw new IncorrectPriceTypeException();
        
        if (!DateTime.TryParse(request.TimeStart, out var timeStart))
            throw new InvalidDateTimeFormatException();
        
        DateTime? timeEnd = null;
        if (!string.IsNullOrEmpty(request.TimeEnd))
        {
            if (!DateTime.TryParse(request.TimeEnd, out var parsedTimeEnd))
                throw new InvalidDateTimeFormatException();
            timeEnd = parsedTimeEnd;
        }
        
        var rent = Rent.Create(
            transport.Id, 
            account.Id,
            type, 
            request.PriceOfUnit,
            timeStart,
            timeEnd,
            request.FinalPrice
        );

        await _adminTransportService.StartRent(transport);
        await _rentRepository.AddAsync(rent);

        return rent;
    }
    
    public async Task<Rent> UpdateAsync(long id, AdminUpdateRentRequest request)
    {
        var rent = await GetByIdAsync(id);
        
        var transport = await _adminTransportService.GetByIdAsync(request.TransportId);
        
        var account = await _adminAccountService.GetByIdAsync(request.UserId);
        
        if (!Enum.TryParse<PriceType>(request.PriceType, true, out var type))
            throw new IncorrectPriceTypeException();
        
        if (!DateTime.TryParse(request.TimeStart, out var timeStart))
            throw new InvalidDateTimeFormatException();
        
        if (!DateTime.TryParse(request.TimeEnd, out var timeEnd))
            throw new InvalidDateTimeFormatException();
        
        var price = type switch
        {
            PriceType.Minutes => transport.MinutePrice,
            PriceType.Days => transport.DayPrice,
            PriceType.None => throw new InvalidEnumArgumentException(),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (price == null)
            throw new InvalidCredentialsRentException();
        
        //При имзенении транспорта админом надо менять canBeRented старого транспорта на false и локацию
        //На уточнении, в тз не учтена локация при изменении транспортного средства
        /*if (rent.TransportId != request.TransportId)
        {
            var lastTransport = await _adminTransportService.GetByIdAsync(rent.TransportId);
            await _adminTransportService.EndRent(lastTransport, request.Latitude, request.Longitude);
            await _adminTransportService.StartRent(transport);
        }*/
        
        var updatedRent = rent.Update(
            transport.Id, 
            account.Id,
            type, 
            price.Value,
            timeStart, 
            timeEnd,
            request.FinalPrice
        );

        await _rentRepository.UpdateAsync(updatedRent);

        return updatedRent;
    }
    
    public async Task DeleteAsync(long rentId)
    {
        if (await _rentRepository.GetByIdAsync(rentId) is not { } rent)
            throw new NotFoundRentException();
        
        await _rentRepository.DeleteAsync(rent);
    }
}
    
    