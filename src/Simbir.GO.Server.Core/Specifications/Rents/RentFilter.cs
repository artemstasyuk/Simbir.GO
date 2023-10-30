using Simbir.GO.Server.ApplicationCore.Specifications.Base;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public class RentFilter: BaseFilter
{
    public int Start { get; set; }
    
    public int Count { get; set; }
    
    public string TransportType { get; set; }
}

