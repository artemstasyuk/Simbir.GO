using Simbir.GO.Server.ApplicationCore.Interfaces.Helpers;

namespace Simbir.GO.Infrastructure.Helpers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}