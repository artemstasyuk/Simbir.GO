using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Simbir.GO.Infrastructure.Persistence.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Simbir.GO.Infrastructure.Auth;
using Simbir.GO.Infrastructure.Helpers;
using Simbir.GO.Infrastructure.Persistence;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces.Helpers;
using Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;

namespace Simbir.GO.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, 
        IHostBuilder host)
    {
        services
            .AddAuth(configuration)
            .AddPersistence(configuration)
            .AddLogging(configuration, host);
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        
        
        return services;
    }
    
    private static IServiceCollection AddAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddScoped<ITokenizer, Tokenizer>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
    
    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        services.AddScoped(typeof(IRepository<>),typeof(EfRepository<>));
        
       
        return services;
    }
    
    private static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration,
        IHostBuilder host)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        host.UseSerilog(logger);

        return services;
    }
}