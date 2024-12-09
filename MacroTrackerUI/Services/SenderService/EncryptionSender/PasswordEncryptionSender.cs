using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using MacroTrackerUI.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace MacroTrackerUI.Services.SenderService.EncryptionSender;

/// <summary>
/// Service for handling password encryption and decryption.
/// </summary>
public class PasswordEncryptionSender
{
    private PasswordEncryptionReceiver Receiver =
        ProviderUI.GetServiceProvider().GetRequiredService<PasswordEncryptionReceiver>();

    /// <summary>
    /// Decrypts the password from local storage.
    /// </summary>
    /// <param name="encryptedPasswordInBase64">The encrypted password in base64 format.</param>
    /// <param name="entropyInBase64">The entropy used during encryption in base64 format.</param>
    /// <returns>The decrypted raw password.</returns>
    public string DecryptPasswordFromLocalStorage(string encryptedPasswordInBase64, string entropyInBase64)
    {
        string passwordJsonSend = JsonSerializer.Serialize((encryptedPasswordInBase64, entropyInBase64));

        string jsonResult = Receiver.DecryptPasswordFromLocalStorage(passwordJsonSend);
        return JsonSerializer.Deserialize<string>(jsonResult);
    }

    /// <summary>
    /// Encrypts the password for database storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>The encrypted password.</returns>
    public string EncryptPasswordToDatabase(string rawPassword)
    {
        string jsonResult = Receiver.EncryptPasswordToDatabase(JsonSerializer.Serialize(rawPassword));
        return JsonSerializer.Deserialize<string>(jsonResult);
    }

    /// <summary>
    /// Encrypts the password for local storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>A tuple containing the encrypted password and entropy, both in base64 format.</returns>
    public (string, string) EncryptPasswordToLocalStorage(string rawPassword)
    {
        string jsonResult = Receiver.EncryptPasswordToDatabase(JsonSerializer.Serialize(rawPassword));
        return JsonSerializer.Deserialize<(string, string)>(jsonResult);
    }
}
