namespace Simbir.GO.Server.ApplicationCore.Contracts.Admin.Accounts;

public record AdminUpdateAccountRequest(
    string Username,
    string Password,
    bool IsAdmin, 
    double Balance
);