namespace Simbir.GO.Server.ApplicationCore.Constants;

public static class SecurityConstants
{
    /// <summary>
    /// Gets a password salt key size
    /// </summary>
    public const int PasswordSaltKeySize = 5;
    
    /// <summary>
    /// Gets a default hash format for account password
    /// </summary>
    public static string DefaultHashedPasswordFormat => "SHA512";
}