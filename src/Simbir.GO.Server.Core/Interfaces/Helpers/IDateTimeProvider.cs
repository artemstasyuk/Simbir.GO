namespace Simbir.GO.Server.ApplicationCore.Interfaces.Helpers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}