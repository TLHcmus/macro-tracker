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
    private IPasswordEncryption Encryptor { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordEncryptionReceiver"/> class.
    /// </summary>
    public PasswordEncryptionReceiver()
    {
        ServiceProvider = ProviderCore.GetServiceProvider();
        Encryptor = ServiceProvider.GetRequiredService<IPasswordEncryption>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordEncryptionReceiver"/> class with a specified service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use.</param>
    public PasswordEncryptionReceiver(ServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Encryptor = serviceProvider.GetRequiredService<IPasswordEncryption>();
    }

    /// <summary>
    /// Encrypts the password using DataProtection API for local storage.
    /// </summary>
    /// <param name="rawPasswordJson">The raw password in JSON format to encrypt.</param>
    /// <returns>A JSON string containing a pair of strings: the first string is the encrypted password in base64 and the second string is the entropy in base64.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the raw password is null.</exception>
    public string EncryptPasswordToLocalStorage(string rawPasswordJson)
    {
        string rawPassword = JsonSerializer.Deserialize<string>(rawPasswordJson);
        if (rawPassword == null)
        {
            throw new ArgumentNullException(nameof(rawPassword));
        }

        var encryptedData = Encryptor.EncryptPasswordToLocalStorage(rawPassword);
        return JsonSerializer.Serialize(encryptedData, new JsonSerializerOptions { IncludeFields = true });
    }

    /// <summary>
    /// Decrypts the password using DataProtection API from local storage.
    /// </summary>
    /// <param name="passwordJson">The JSON string containing the encrypted password and entropy in base64.</param>
    /// <returns>A JSON string containing the raw password.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the encrypted password or entropy is null.</exception>
    public string DecryptPasswordFromLocalStorage(string passwordJson)
    {
        var encryptedData = JsonSerializer.Deserialize<(string EncryptedPassword, string Entropy)>(passwordJson, new JsonSerializerOptions { IncludeFields = true });

        if (encryptedData.EncryptedPassword == null || encryptedData.Entropy == null)
        {
            throw new ArgumentNullException();
        }

        string rawPassword = Encryptor.DecryptPasswordFromLocalStorage(encryptedData.EncryptedPassword, encryptedData.Entropy);
        return JsonSerializer.Serialize(rawPassword);
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
