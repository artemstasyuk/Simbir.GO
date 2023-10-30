using Microsoft.Extensions.DependencyInjection;
using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Services;
using Simbir.GO.Server.ApplicationCore.Services.Admin;


namespace Simbir.GO.Server.ApplicationCore;

public static class Dependencies
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IRentService, RentService>();
        services.AddScoped<ITransportService, TransportService>();
        return services;
    }


    public static IServiceCollection AddAdmin(this IServiceCollection services)
    {
        services.AddScoped<IAdminAccountService, AdminAccountService>();
        services.AddScoped<IAdminRentService, IAdminRentService>();
        services.AddScoped<IAdminTransportService,  AdminTransportService>();

        return services;
    }
}