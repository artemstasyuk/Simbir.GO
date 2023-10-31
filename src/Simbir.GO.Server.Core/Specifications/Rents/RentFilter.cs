using Simbir.GO.Server.ApplicationCore.Specifications.Base;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Rents;

public class RentFilter: BaseFilter
{
    public string? TransportType { get; set; }
}

