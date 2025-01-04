using System.Security.Cryptography;
using System.Text;

namespace MacroTrackerCore.Services.EncryptionService;

/// <summary>
/// This class is used to encrypt and decrypt passwords.
/// </summary>
public class PasswordEncryption : IPasswordEncryption
{
    /// <summary>
    /// Encrypts the raw password for local storage using the DataProtection API.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>
    /// A tuple containing the encrypted password in Base64 format and the entropy used for encryption in Base64 format.
    /// </returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the platform is not Windows.</exception>
    public (string EncryptedPassword, string Entropy) EncryptPasswordToLocalStorage(string rawPassword)
    {
        EnsureWindowsPlatform();

        byte[] passwordInBytes = Encoding.UTF8.GetBytes(rawPassword);
        byte[] entropyInBytes = GenerateEntropy();

        byte[] encryptedPasswordInBytes = ProtectedData.Protect(passwordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);

        return (Convert.ToBase64String(encryptedPasswordInBytes), Convert.ToBase64String(entropyInBytes));
    }

    /// <summary>
    /// Decrypts the encrypted password from local storage using the DataProtection API.
    /// </summary>
    /// <param name="encryptedPasswordInBase64">The encrypted password in Base64 format.</param>
    /// <param name="entropyInBase64">The entropy used for encryption in Base64 format.</param>
    /// <returns>The decrypted raw password.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the platform is not Windows.</exception>
    public string DecryptPasswordFromLocalStorage(string encryptedPasswordInBase64, string entropyInBase64)
    {
        EnsureWindowsPlatform();

        byte[] encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordInBase64);
        byte[] entropyInBytes = Convert.FromBase64String(entropyInBase64);

        byte[] decryptedPasswordInBytes = ProtectedData.Unprotect(encryptedPasswordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);

        return Encoding.UTF8.GetString(decryptedPasswordInBytes);
    }

    /// <summary>
    /// Encrypts the raw password for database storage using SHA256.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>The encrypted password in a format suitable for database storage.</returns>
    public string EncryptPasswordToDatabase(string rawPassword)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
        return ConvertToHexString(hashBytes);
    }

    /// <summary>
    /// Ensures the current platform is Windows.
    /// </summary>
    /// <exception cref="PlatformNotSupportedException">Thrown when the platform is not Windows.</exception>
    private static void EnsureWindowsPlatform()
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException("DataProtection API is only supported on Windows.");
        }
    }

    /// <summary>
    /// Generates a random entropy value.
    /// </summary>
    /// <returns>A byte array containing the entropy.</returns>
    private static byte[] GenerateEntropy()
    {
        byte[] entropyInBytes = new byte[20];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(entropyInBytes);
        }
        return entropyInBytes;
    }

    /// <summary>
    /// Converts a byte array to a hexadecimal string.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <returns>A hexadecimal string representation of the byte array.</returns>
    private static string ConvertToHexString(byte[] bytes)
    {
        StringBuilder builder = new();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}
