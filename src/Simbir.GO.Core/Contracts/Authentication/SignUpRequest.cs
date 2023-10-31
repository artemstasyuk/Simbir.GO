namespace Simbir.GO.Server.ApplicationCore.Contracts.Authentication;

public record SignUpRequest(
    string Username, 
    string Password
);