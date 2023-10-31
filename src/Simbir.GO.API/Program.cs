using Simbir.GO.API;
using Simbir.GO.API.Middlewares;
using Simbir.GO.Server.ApplicationCore;
using Simbir.GO.Infrastructure;
using Simbir.GO.Infrastructure.Persistence;
using Simbir.GO.Infrastructure.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationCore()
    .AddInfrastructure(builder.Configuration, builder.Host)
    .AddPresentation();


var app = builder.Build();

await builder.Services.AddSeed();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedAsync(catalogContext);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());
    
    app.UseExceptionHandler("/error"); 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseMiddleware<AccessTokeValidationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();