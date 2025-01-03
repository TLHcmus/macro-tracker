using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MacroTrackerUITest.Services.ProviderService;

[TestClass]
public class ProviderUITests
{
    [TestMethod]
    public void GetServiceProvider_ShouldReturnServiceProvider()
    {
        // Act
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Assert
        Assert.IsNotNull(serviceProvider);
    }

    [TestMethod]
    public void GetServiceProvider_ShouldContainPasswordEncryptionReceiver()
    {
        // Arrange
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IPasswordEncryptionReceiver>();

        // Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetServiceProvider_ShouldContainDaoReceiver()
    {
        // Arrange
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Act
        var service = serviceProvider.GetService<IDaoReceiver>();

        // Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetServiceProvider_ShouldContainPasswordEncryptionSender()
    {
        // Arrange
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IPasswordEncryptionSender>();

        // Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetServiceProvider_ShouldContainDaoSender()
    {
        // Arrange
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<IDaoSender>();

        // Assert
        Assert.IsNotNull(service);
    }
}
