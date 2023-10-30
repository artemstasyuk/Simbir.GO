using Simbir.GO.Server.Domain.Common.Entity;

namespace Simbir.GO.Server.Domain.Transports.ValueObjects;

/// <summary>
/// Represents a location
/// </summary>
public class Location : ValueObject
{
    /// <summary>
    ///  Gets or sets Latitude 
    /// </summary>
    public double Latitude { get; private set; }
    
    /// <summary>
    ///  Gets or sets Longitude 
    /// </summary>
    public double Longitude { get; private set; }

    /// <summary>
    ///  Ctor 
    /// </summary>
    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Location Create(double latitude, double longitude) =>
        new(latitude, longitude);

    public  Location Update(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;

        return this;
    }
    
    
    public static double CalculateDistance(double searchLatitude, double searchLongitude, Location transportLocation)
    {
        var latitudeDifference = Math.Abs(searchLatitude - transportLocation.Latitude);
        var longitudeDifference = Math.Abs(searchLongitude - transportLocation.Longitude);
        var distance = Math.Sqrt(Math.Pow(latitudeDifference, 2) + Math.Pow(longitudeDifference, 2));
            
        return distance;
    }
    
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Latitude;
        yield return Longitude;
    }
}