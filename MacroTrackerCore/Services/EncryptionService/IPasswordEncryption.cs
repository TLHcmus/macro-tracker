namespace MacroTrackerCore.Services.EncryptionService
{
    /// <summary>
    /// Interface for password encryption services.
    /// Provides methods to encrypt and decrypt passwords for local storage and database storage.
    /// </summary>
    public interface IPasswordEncryption
    {
        /// <summary>
        /// Encrypts the raw password for local storage.
        /// </summary>
        /// <param name="rawPassword">The raw password to encrypt.</param>
        /// <returns>
        /// A tuple containing the encrypted password in Base64 format and the entropy used for encryption in Base64 format.
        /// </returns>
        (string EncryptedPassword, string Entropy) EncryptPasswordToLocalStorage(string rawPassword);
        /// <summary>
        /// Decrypts the encrypted password from local storage.
        /// </summary>
        /// <param name="encryptedPasswordInBase64">The encrypted password in Base64 format.</param>
        /// <param name="entropyInBase64">The entropy used for encryption in Base64 format.</param>
        /// <returns>The decrypted raw password.</returns>
        string DecryptPasswordFromLocalStorage(string encryptedPasswordInBase64, string entropyInBase64);
        /// <summary>
        /// Encrypts the raw password for database storage.
        /// </summary>
        /// <param name="rawPassword">The raw password to encrypt.</param>
        /// <returns>The encrypted password in a format suitable for database storage.</returns>
        string EncryptPasswordToDatabase(string rawPassword);
    }
}
