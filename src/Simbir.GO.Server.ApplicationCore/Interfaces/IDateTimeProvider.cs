namespace Simbir.GO.Server.ApplicationCore.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}