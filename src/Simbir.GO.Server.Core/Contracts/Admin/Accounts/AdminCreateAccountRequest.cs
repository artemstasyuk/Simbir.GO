namespace Simbir.GO.Server.ApplicationCore.Contracts.Admin.Accounts;

public record AdminCreateAccountRequest(
    string Username,
    string Password,
    bool IsAdmin, 
    double Balance
);