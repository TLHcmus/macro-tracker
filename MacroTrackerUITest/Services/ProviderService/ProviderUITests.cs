using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var service = serviceProvider.GetRequiredService<PasswordEncryptionReceiver>();

        // Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetServiceProvider_ShouldContainDaoReceiver()
    {
        // Arrange
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Act
        var service = serviceProvider.GetService<DaoReceiver>();

        // Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetServiceProvider_ShouldContainPasswordEncryptionSender()
    {
        // Arrange
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<PasswordEncryptionSender>();

        // Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetServiceProvider_ShouldContainDaoSender()
    {
        // Arrange
        var serviceProvider = ProviderUI.GetServiceProvider();

        // Act
        var service = serviceProvider.GetRequiredService<DaoSender>();

        // Assert
        Assert.IsNotNull(service);
    }
}
