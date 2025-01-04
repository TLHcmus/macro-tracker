using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Text.Json;

namespace MacroTrackerUITest.Services.SenderService.EncryptionSender;

[TestClass]
public class PasswordEncryptionSenderTests
{
    private Mock<IPasswordEncryptionReceiver> _mockReceiver;
    private PasswordEncryptionSender _sender;

    [TestInitialize]
    public void Setup()
    {
        _mockReceiver = new Mock<IPasswordEncryptionReceiver>();
        var serviceProvider = new Mock<IServiceProvider>();
        serviceProvider.Setup(
            x => x.GetService(typeof(IPasswordEncryptionReceiver))
        ).Returns(_mockReceiver.Object);
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(_mockReceiver.Object);
        if (serviceProvider.Object != null)
        {
            serviceCollection.AddSingleton(serviceProvider.Object);
        }

        _sender = new PasswordEncryptionSender(serviceCollection.BuildServiceProvider());
    }

    [TestMethod]
    public void DecryptPasswordFromLocalStorage_ShouldReturnDecryptedPassword()
    {
        // Arrange
        string encryptedPasswordInBase64 = "encryptedPassword";
        string entropyInBase64 = "entropy";
        string expectedPassword = "rawPassword";
        string passwordJsonSend = JsonSerializer.Serialize((encryptedPasswordInBase64, entropyInBase64), new JsonSerializerOptions { IncludeFields = true });
        _mockReceiver.Setup(
            x => x.DecryptPasswordFromLocalStorage(passwordJsonSend)
        ).Returns(JsonSerializer.Serialize(expectedPassword));

        // Act
        string result = _sender.DecryptPasswordFromLocalStorage(encryptedPasswordInBase64, entropyInBase64);

        // Assert
        Assert.AreEqual(expectedPassword, result);
    }

    [TestMethod]
    public void EncryptPasswordToLocalStorage_ShouldReturnEncryptedPasswordAndEntropy()
    {
        // Arrange
        string rawPassword = "rawPassword";
        var expectedResult = ("encryptedPassword", "entropy");
        string jsonResult = JsonSerializer.Serialize(
            expectedResult,
            new JsonSerializerOptions
            {
                IncludeFields = true
            }
        );
        _mockReceiver.Setup(
            x => x.EncryptPasswordToLocalStorage(
                It.Is<string>(
                    s => s == JsonSerializer.Serialize(
                        rawPassword, 
                        new JsonSerializerOptions { IncludeFields = true }
                    )
                )
            )
        ).Returns(jsonResult);

        // Act
        var result = _sender.EncryptPasswordToLocalStorage(rawPassword);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]
    public void EncryptPasswordToDatabase_ShouldReturnEncryptedPassword()
    {
        // Arrange
        string rawPassword = "rawPassword";
        string expectedEncryptedPassword = "encryptedPassword";
        _mockReceiver.Setup(x => x.EncryptPasswordToDatabase(rawPassword)).Returns(expectedEncryptedPassword);

        // Act
        string result = _sender.EncryptPasswordToDatabase(rawPassword);

        // Assert
        Assert.AreEqual(expectedEncryptedPassword, result);
    }
}
