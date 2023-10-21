using Simbir.GO.Server.ApplicationCore.Interfaces;

namespace Simbir.GO.Server.Infrastructure.Helpers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}