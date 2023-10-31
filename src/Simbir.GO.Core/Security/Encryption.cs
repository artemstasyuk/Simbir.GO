using System.Security.Cryptography;
using System.Text;
using Simbir.GO.Server.ApplicationCore.Helpers;

namespace Simbir.GO.Server.ApplicationCore.Security;

/// <summary>
/// Encryption service
/// </summary>
public static class Encryption
{
    /// <summary>
    /// Create salt key
    /// </summary>
    /// <param name="size">Key size</param>
    /// <returns>Salt key</returns>
    public static string CreateSaltKey(int size)
    {
        //generate a cryptographic random number
        using var provider = RandomNumberGenerator.Create();
        var buff = new byte[size];
        provider.GetBytes(buff);

        // Return a Base64 string representation of the random number
        return Convert.ToBase64String(buff);
    }

    /// <summary>
    /// Create a password hash
    /// </summary>
    /// <param name="password">Password</param>
    /// <param name="saltkey">Salt key</param>
    /// <param name="passwordFormat">Password format (hash algorithm)</param>
    /// <returns>Password hash</returns>
    public static string CreatePasswordHash(string password, string saltkey, string passwordFormat)
    {
        return HashHelper.CreateHash(Encoding.UTF8.GetBytes(string.Concat(password, saltkey)), passwordFormat);
    }
}