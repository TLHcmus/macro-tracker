using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using MacroTrackerUI.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace MacroTrackerUI.Services.SenderService.EncryptionSender;

/// <summary>
/// Service for handling password encryption and decryption.
/// </summary>
public class PasswordEncryptionSender : IPasswordEncryptionSender
{
    public IServiceProvider ProviderUI { get; }
    private readonly IPasswordEncryptionReceiver _receiver;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordEncryptionSender"/> class.
    /// </summary>
    public PasswordEncryptionSender()
    {
        ProviderUI = ProviderService.ProviderUI.GetServiceProvider();
        _receiver = ProviderUI.GetRequiredService<IPasswordEncryptionReceiver>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordEncryptionSender"/> class with a specified service provider.
    /// </summary>
    /// <param name="providerUI">The service provider.</param>
    public PasswordEncryptionSender(ServiceProvider providerUI)
    {
        ProviderUI = providerUI;
        _receiver = ProviderUI.GetRequiredService<IPasswordEncryptionReceiver>();
    }

    /// <summary>
    /// Decrypts the password from local storage.
    /// </summary>
    /// <param name="encryptedPasswordInBase64">The encrypted password in base64 format.</param>
    /// <param name="entropyInBase64">The entropy used during encryption in base64 format.</param>
    /// <returns>The decrypted raw password.</returns>
    public string DecryptPasswordFromLocalStorage(string encryptedPasswordInBase64, string entropyInBase64)
    {
        var options = new JsonSerializerOptions { IncludeFields = true };
        var passwordJsonSend = JsonSerializer.Serialize((encryptedPasswordInBase64, entropyInBase64), options);
        var jsonResult = _receiver.DecryptPasswordFromLocalStorage(passwordJsonSend);
        return JsonSerializer.Deserialize<string>(jsonResult);
    }

    /// <summary>
    /// Encrypts the password for local storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>A tuple containing the encrypted password and entropy, both in base64 format.</returns>
    public (string, string) EncryptPasswordToLocalStorage(string rawPassword)
    {
        var jsonResult = _receiver.EncryptPasswordToLocalStorage(JsonSerializer.Serialize(rawPassword));
        var options = new JsonSerializerOptions { IncludeFields = true };
        return JsonSerializer.Deserialize<(string, string)>(jsonResult, options);
    }

    /// <summary>
    /// Encrypts the password for database storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>The encrypted password in a format suitable for database storage.</returns>
    public string EncryptPasswordToDatabase(string rawPassword)
    {
        return _receiver.EncryptPasswordToDatabase(rawPassword);
    }
}
