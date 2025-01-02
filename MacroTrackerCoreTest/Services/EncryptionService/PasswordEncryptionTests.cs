using MacroTrackerCore.Services.EncryptionService;

namespace MacroTrackerCoreTest;

[TestClass]
public class PasswordEncryptionTests
{
    private PasswordEncryption _passwordEncryption;

    [TestInitialize]
    public void Setup()
    {
        _passwordEncryption = new PasswordEncryption();
    }

    [TestMethod]
    public void EncryptPasswordToLocalStorage_ShouldReturnEncryptedPasswordAndEntropy()
    {
        // Arrange
        string rawPassword = "TestPassword123";

        // Act
        (var encryptedPassword, var entropy) = _passwordEncryption.EncryptPasswordToLocalStorage(rawPassword);

        // Assert
        Assert.IsNotNull(encryptedPassword);
        Assert.IsNotNull(entropy);
        Assert.AreNotEqual(rawPassword, encryptedPassword);
    }

    [TestMethod]
    public void DecryptPasswordFromLocalStorage_ShouldReturnOriginalPassword()
    {
        // Arrange
        string rawPassword = "TestPassword123";
        (var encryptedPassword, var entropy) = _passwordEncryption.EncryptPasswordToLocalStorage(rawPassword);

        // Act
        string decryptedPassword = _passwordEncryption.DecryptPasswordFromLocalStorage(encryptedPassword, entropy);

        // Assert
        Assert.AreEqual(rawPassword, decryptedPassword);
    }

    [TestMethod]
    public void EncryptPasswordToLocalStorage_ShouldThrowPlatformNotSupportedException_OnNonWindowsPlatform()
    {
        // Arrange
        string rawPassword = "TestPassword123";

        // Act
        if (!OperatingSystem.IsWindows())
            Assert.ThrowsException<PlatformNotSupportedException>(
                () => _passwordEncryption.EncryptPasswordToLocalStorage(rawPassword)
            );
        else
            Assert.IsTrue(true);
    }

    [TestMethod]
    public void DecryptPasswordFromLocalStorage_ShouldThrowPlatformNotSupportedException_OnNonWindowsPlatform()
    {
        // Arrange
        string encryptedPassword = "EncryptedPassword";
        string entropy = "Entropy";

        // Act
        if (!OperatingSystem.IsWindows())
            Assert.ThrowsException<PlatformNotSupportedException>(
                () => _passwordEncryption.DecryptPasswordFromLocalStorage(encryptedPassword, entropy)
            );
        else
            Assert.IsTrue(true);
    }

    [TestMethod]
    public void EncryptPasswordToDatabase_ShouldReturnHashedPassword()
    {
        // Arrange
        string rawPassword = "TestPassword123";

        // Act
        string hashedPassword = _passwordEncryption.EncryptPasswordToDatabase(rawPassword);

        // Assert
        Assert.IsNotNull(hashedPassword);
        Assert.AreNotEqual(rawPassword, hashedPassword);
        Assert.AreEqual(64, hashedPassword.Length); // SHA256 hash length in hex is 64 characters
    }
}
