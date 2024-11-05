using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Helpers
{
    /// <summary>
    /// This class is used to encrypt password
    /// </summary>
    public class PasswordEncryptionHelper
    {
        /// <summary>
        /// Encrypt password using DataProtection API
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <returns>
        /// A pair of strings: the first string is the encrypted password in base64 and the second string is the entropy in base64
        /// </returns>
        static public (string, string) EncryptPasswordToLocalStorage(string rawPassword)
        {
            Byte[] passwordInBytes = Encoding.UTF8.GetBytes(rawPassword);
            Byte[] entropyInBytes = new byte[20];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(entropyInBytes);
            }

            Byte[] encryptedPasswordInBytes = ProtectedData.Protect(passwordInBytes, 
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
        static public string DecryptPasswordFromLocalStorage(string encryptedPasswordInBase64, string entropyInBase64)
        {
            Byte[] encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordInBase64);
            Byte[] entropyInBytes = Convert.FromBase64String(entropyInBase64);

            Byte[] decryptedPasswordInBytes = ProtectedData.Unprotect(encryptedPasswordInBytes,
                                                                      entropyInBytes,
                                                                      DataProtectionScope.CurrentUser);

            return Encoding.UTF8.GetString(decryptedPasswordInBytes);
        }

        /// <summary>
        /// Encrypt password using SHA256 to store in database
        /// </summary>
        /// <param name="rawPassword"></param>
        /// <returns></returns>
        static public string EncryptPasswordToDatabase(string rawPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
