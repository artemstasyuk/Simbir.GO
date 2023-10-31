namespace Simbir.GO.Server.ApplicationCore.Contracts.Authentication;

public record SignInRequest(
    string Username,
    string Password
);