namespace Simbir.GO.Server.ApplicationCore.Contracts.Accounts;

public record EditAccountRequest(
    string Username, 
    string Password
);