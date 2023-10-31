using Simbir.GO.Server.ApplicationCore.Contracts.Rents;
using Simbir.GO.Server.ApplicationCore.Contracts.Transports;

namespace Simbir.GO.Server.ApplicationCore.Contracts.Accounts;

public record AccountResult(
    long Id,
    string Username,
    string Role, 
    double Balance,
    List<TransportResult> AccountTransports,
    List<RentResult> AccountRents
);