using Simbir.GO.Domain.Common.Entity;
using Simbir.GO.Domain.Transports.Errors;

namespace Simbir.GO.Domain.Transports.ValueObjects;

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

    public static Location Create(double latitude, double longitude)
    {
        if (latitude is < -90 or > 90)
            throw new OutOfRangeLocationException();
        
        if (latitude is < -180 or > 180)
            throw new OutOfRangeLocationException();
       
        return new(latitude, longitude);
    }

    public Location Update(double latitude, double longitude)
    {
        if (latitude is < -90 or > 90)
            throw new OutOfRangeLocationException();
        
        if (latitude is < -180 or > 180)
            throw new OutOfRangeLocationException();
        
        Latitude = latitude;
        Longitude = longitude;

        return this;
    }
    
    public double CalculateDistance(double searchLatitude, double searchLongitude, Location transportLocation)
    {
        if (searchLatitude is < -90 or > 90)
            throw new OutOfRangeLocationException();
        
        if (searchLongitude is < -180 or > 180)
            throw new OutOfRangeLocationException();
        
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