namespace MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;

/// <summary>
/// Interface for password encryption services.
/// Provides methods to encrypt and decrypt passwords for local storage and database storage.
/// </summary>
public interface IPasswordEncryptionReceiver
{
    /// <summary>
    /// Encrypts the raw password for local storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>
    /// A string containing the encrypted password in Base64 format.
    /// </returns>
    string EncryptPasswordToLocalStorage(string rawPassword);

    /// <summary>
    /// Decrypts the encrypted password from local storage.
    /// </summary>
    /// <param name="passwordJson">The JSON string containing the encrypted password and entropy in Base64 format.</param>
    /// <returns>The decrypted raw password.</returns>
    string DecryptPasswordFromLocalStorage(string passwordJson);

    /// <summary>
    /// Encrypts the raw password for database storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>The encrypted password in a format suitable for database storage.</returns>
    string EncryptPasswordToDatabase(string rawPassword);
}
