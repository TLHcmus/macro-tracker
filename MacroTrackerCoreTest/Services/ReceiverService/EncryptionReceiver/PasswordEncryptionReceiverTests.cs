using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Text.Json;

namespace MacroTrackerCoreTest.Services.ReceiverService.EncryptionReceiver;

[TestClass]
public class PasswordEncryptionReceiverTests
{
    private Mock<IPasswordEncryption> _mockEncryptor;
    private PasswordEncryptionReceiver _receiver;

    [TestInitialize]
    public void Setup()
    {
        _mockEncryptor = new Mock<IPasswordEncryption>();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(_mockEncryptor.Object)
            .BuildServiceProvider();

        _receiver = new PasswordEncryptionReceiver(serviceProvider);
    }

    [TestMethod]
    public void EncryptPasswordToLocalStorage_ShouldThrowArgumentNullException_WhenRawPasswordJsonIsNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _receiver.EncryptPasswordToLocalStorage(null));
    }

    [TestMethod]
    public void EncryptPasswordToLocalStorage_ShouldReturnEncryptedPasswordJson()
    {
        // Arrange
        string rawPassword = "password123";
        string rawPasswordJson = JsonSerializer.Serialize(rawPassword);
        var encryptedData = ("encryptedPasswordBase64", "entropyBase64");
        _mockEncryptor.Setup(e => e.EncryptPasswordToLocalStorage(rawPassword)).Returns(encryptedData);

        // Act
        string result = _receiver.EncryptPasswordToLocalStorage(rawPasswordJson);

        // Assert
        var expectedJson = JsonSerializer.Serialize(encryptedData, new JsonSerializerOptions { IncludeFields = true });
        Assert.AreEqual(expectedJson, result);
    }

    [TestMethod]
    public void DecryptPasswordFromLocalStorage_ShouldThrowArgumentNullException_WhenPasswordJsonIsNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _receiver.DecryptPasswordFromLocalStorage(null));
    }

    [TestMethod]
    public void DecryptPasswordFromLocalStorage_ShouldReturnRawPasswordJson()
    {
        // Arrange
        (var encryptedPasswordBase64, var entropyBase64) = ("encryptedPasswordBase64", "entropyBase64");
        string passwordJson = JsonSerializer.Serialize(
            (encryptedPasswordBase64, entropyBase64), 
            new JsonSerializerOptions { IncludeFields = true }
        );
        string rawPassword = "password123";
        _mockEncryptor.Setup(
            e => e.DecryptPasswordFromLocalStorage(encryptedPasswordBase64, entropyBase64)
        ).Returns(rawPassword);

        // Act
        string result = _receiver.DecryptPasswordFromLocalStorage(passwordJson);

        // Assert
        var expectedJson = JsonSerializer.Serialize(rawPassword);
        Assert.AreEqual(expectedJson, result);
    }

    [TestMethod]
    public void EncryptPasswordToDatabase_ShouldThrowArgumentNullException_WhenRawPasswordIsNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _receiver.EncryptPasswordToDatabase(null));
    }

    [TestMethod]
    public void EncryptPasswordToDatabase_ShouldReturnEncryptedPassword()
    {
        // Arrange
        string rawPassword = "password123";
        string encryptedPassword = "encryptedPassword";
        _mockEncryptor.Setup(e => e.EncryptPasswordToDatabase(rawPassword)).Returns(encryptedPassword);

        // Act
        string result = _receiver.EncryptPasswordToDatabase(rawPassword);

        // Assert
        Assert.AreEqual(encryptedPassword, result);
    }
}
