using System.Security.Cryptography;
using System.Text;

namespace MacroTrackerCore.Services.EncryptionService
{
    /// <summary>
    /// This class is used to encrypt password
    /// </summary>
    public class PasswordEncryption : IPasswordEncryption
    {
        /// <summary>
        /// Encrypt password using DataProtection API
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <returns>
        /// A pair of strings: the first string is the encrypted password in base64 and the second string is the entropy in base64
        /// </returns>
        /// <exception cref="PlatformNotSupportedException">Thrown when the platform is not Windows.</exception>
        public (string, string) EncryptPasswordToLocalStorage(string rawPassword)
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException("DataProtection API is only supported on Windows.");
            }

            byte[] passwordInBytes = Encoding.UTF8.GetBytes(rawPassword);
            byte[] entropyInBytes = new byte[20];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(entropyInBytes);
            }

            byte[] encryptedPasswordInBytes = ProtectedData.Protect(passwordInBytes,
                                                                    entropyInBytes,
                                                                    DataProtectionScope.CurrentUser);

            var encryptedPasswordInBase64 = Convert.ToBase64String(encryptedPasswordInBytes);
            var entropyInBase64 = Convert.ToBase64String(entropyInBytes);
            return (encryptedPasswordInBase64, entropyInBase64);
        }

        /// <summary>
        /// Decrypt password using DataProtection API
        /// </summary>
        /// <param name="encryptedPasswordInBase64"></param>
        /// <param name="entropyInBase64"></param>
        /// <returns>Raw password</returns>
        /// <exception cref="PlatformNotSupportedException">Thrown when the platform is not Windows.</exception>
        public string DecryptPasswordFromLocalStorage(string encryptedPasswordInBase64, string entropyInBase64)
        {
            if (!OperatingSystem.IsWindows())
            {
                throw new PlatformNotSupportedException("DataProtection API is only supported on Windows.");
            }

            byte[] encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordInBase64);
            byte[] entropyInBytes = Convert.FromBase64String(entropyInBase64);

            byte[] decryptedPasswordInBytes = ProtectedData.Unprotect(encryptedPasswordInBytes,
                                                                      entropyInBytes,
                                                                      DataProtectionScope.CurrentUser);

            return Encoding.UTF8.GetString(decryptedPasswordInBytes);
        }

        /// <summary>
        /// Encrypt password using SHA256 to store in database
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <returns></returns>
        public string EncryptPasswordToDatabase(string rawPassword)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
            StringBuilder builder = new();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
