namespace Simbir.GO.Server.ApplicationCore.Contracts.Authentication;

public record AuthResult(
    string Username, 
    string Token
);
