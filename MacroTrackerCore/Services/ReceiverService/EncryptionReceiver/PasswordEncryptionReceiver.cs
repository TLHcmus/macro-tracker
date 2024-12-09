using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;

/// <summary>
/// Service for handling password encryption and decryption.
/// </summary>
public class PasswordEncryptionReceiver
{
    /// <summary>
    /// Gets or sets the password encryption service.
    /// </summary>
    IPasswordEncryption Encryptor { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordEncryptionReceiver"/> class.
    /// </summary>
    public PasswordEncryptionReceiver()
    {
        Encryptor =
            ProviderCore.GetServiceProvider().GetRequiredService<IPasswordEncryption>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordEncryptionReceiver"/> class with a specified encryption service.
    /// </summary>
    /// <param name="service">The password encryption service.</param>
    public PasswordEncryptionReceiver(IPasswordEncryption service)
        => Encryptor = service;

    /// <summary>
    /// Encrypts the password using DataProtection API for local storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>
    /// A JSON string containing a pair of strings: the first string is the encrypted password in base64 and the second string is the entropy in base64.
    /// </returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the platform is not Windows.</exception>
    public string EncryptPasswordToLocalStorage(string rawPassword)
    {
        return JsonSerializer.Serialize(
            Encryptor.EncryptPasswordToLocalStorage(rawPassword)
        );
    }

    /// <summary>
    /// Decrypts the password using DataProtection API from local storage.
    /// </summary>
    /// <param name="passwordJson">The JSON string containing the encrypted password and entropy in base64.</param>
    /// <returns>A JSON string containing the raw password.</returns>
    /// <exception cref="PlatformNotSupportedException">Thrown when the platform is not Windows.</exception>
    public string DecryptPasswordFromLocalStorage(string passwordJson)
    {
        (string encryptedPasswordInBase64, string entropyInBase64) =
            JsonSerializer.Deserialize<(string, string)>(passwordJson);

        return JsonSerializer.Serialize(
            Encryptor.DecryptPasswordFromLocalStorage(encryptedPasswordInBase64, entropyInBase64)
        );
    }

    /// <summary>
    /// Encrypts the password using SHA256 for database storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>A JSON string containing the encrypted password.</returns>
    public string EncryptPasswordToDatabase(string rawPassword)
    {
        return JsonSerializer.Serialize(
            Encryptor.EncryptPasswordToDatabase(rawPassword)
        );
    }
}
