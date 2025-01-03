﻿namespace MacroTrackerUI.Services.SenderService.EncryptionSender;

/// <summary>
/// Interface for password encryption sender services.
/// Provides methods to encrypt and decrypt passwords for local storage and database storage.
/// </summary>
public interface IPasswordEncryptionSender
{
    /// <summary>
    /// Decrypts the password from local storage.
    /// </summary>
    /// <param name="encryptedPasswordInBase64">The encrypted password in base64 format.</param>
    /// <param name="entropyInBase64">The entropy used during encryption in base64 format.</param>
    /// <returns>The decrypted raw password.</returns>
    string DecryptPasswordFromLocalStorage(string encryptedPasswordInBase64, string entropyInBase64);

    /// <summary>
    /// Encrypts the password for local storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>A tuple containing the encrypted password and entropy, both in base64 format.</returns>
    (string, string) EncryptPasswordToLocalStorage(string rawPassword);

    /// <summary>
    /// Encrypts the password for database storage.
    /// </summary>
    /// <param name="rawPassword">The raw password to encrypt.</param>
    /// <returns>The encrypted password in a format suitable for database storage.</returns>
    string EncryptPasswordToDatabase(string rawPassword);
}
