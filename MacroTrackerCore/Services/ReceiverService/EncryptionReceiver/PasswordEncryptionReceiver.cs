using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;

/// <summary>
/// Service for handling password encryption and decryption.
/// </summary>
public class PasswordEncryptionReceiver : IPasswordEncryptionReceiver
{
    public ServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Gets or sets the password encryption service.
    /// </summary>
    private IPasswordEncryption Encryptor { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordEncryptionReceiver"/> class.
    /// </summary>
    public PasswordEncryptionReceiver()
    {
        ServiceProvider = ProviderCore.GetServiceProvider();
        Encryptor = ServiceProvider.GetRequiredService<IPasswordEncryption>();
    }

    public PasswordEncryptionReceiver(ServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Encryptor = serviceProvider.GetRequiredService<IPasswordEncryption>();
    }

    /// <summary>
    /// Encrypts the password using DataProtection API for local storage.
    /// </summary>
    /// <param name="rawPasswordJson">The raw password in JSON format to encrypt.</param>
    /// <returns>
    /// A JSON string containing a pair of strings: the first string is the encrypted password in base64 and the second string is the entropy in base64.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the raw password is null.</exception>
    public string EncryptPasswordToLocalStorage(string rawPasswordJson)
    {
        string rawPassword = JsonSerializer.Deserialize<string>(rawPasswordJson);
        if (rawPassword == null)
        {
            throw new ArgumentNullException(nameof(rawPassword));
        }

        JsonSerializerOptions options = new()
        {
            IncludeFields = true // Enables serialization of fields
        };

        return JsonSerializer.Serialize(
            Encryptor.EncryptPasswordToLocalStorage(rawPassword),
            options
        );
    }

    /// <summary>
    /// Decrypts the password using DataProtection API from local storage.
    /// </summary>
    /// <param name="passwordJson">The JSON string containing the encrypted password and entropy in base64.</param>
    /// <returns>A JSON string containing the raw password.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the encrypted password or entropy is null.</exception>
    public string DecryptPasswordFromLocalStorage(string passwordJson)
    {
        JsonSerializerOptions options = new()
        {
            IncludeFields = true // Enables serialization of fields
        };
        (string encryptedPasswordInBase64, string entropyInBase64) =
            JsonSerializer.Deserialize<(string, string)>(passwordJson, options);

        if (encryptedPasswordInBase64 == null || entropyInBase64 == null)
        {
            throw new ArgumentNullException();
        }

        return JsonSerializer.Serialize(
            Encryptor.DecryptPasswordFromLocalStorage(encryptedPasswordInBase64, entropyInBase64)
        );
    }

    /// <summary>
    /// Encrypts the password for database storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>The encrypted password in a format suitable for database storage.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the raw password is null.</exception>
    public string EncryptPasswordToDatabase(string rawPassword)
    {
        if (rawPassword == null)
        {
            throw new ArgumentNullException(nameof(rawPassword));
        }

        return Encryptor.EncryptPasswordToDatabase(rawPassword);
    }
}
