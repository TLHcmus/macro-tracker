using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;

namespace MacroTrackerCoreTest.Services.ProviderService;

[TestClass]
public class ProviderCoreTests
{
    [TestMethod]
    public void GetServiceProvider_WhenCalled_ShouldReturnServiceProviderInstance()
    {
        // Act
        var serviceProvider = ProviderCore.GetServiceProvider();

        // Assert
        Assert.IsNotNull(serviceProvider);
    }

    [TestMethod]
    public void GetServiceProvider_WhenCalledTwice_ShouldReturnSameInstance()
    {
        // Act
        var serviceProvider1 = ProviderCore.GetServiceProvider();
        var serviceProvider2 = ProviderCore.GetServiceProvider();

        // Assert
        Assert.AreSame(serviceProvider1, serviceProvider2);
    }

    [TestMethod]
    public void GetServiceProvider_WhenCalled_ShouldResolveIDao()
    {
        // Arrange
        var serviceProvider = ProviderCore.GetServiceProvider();

        // Act
        var dao = serviceProvider.GetRequiredService<IDao>();

        // Assert
        Assert.IsNotNull(dao);
        Assert.IsInstanceOfType(dao, typeof(DatabaseDao));
    }

    [TestMethod]
    public void GetServiceProvider_WhenCalled_ShouldResolveIPasswordEncryption()
    {
        // Arrange
        var serviceProvider = ProviderCore.GetServiceProvider();

        // Act
        var encryptionService = serviceProvider.GetRequiredService<IPasswordEncryption>();

        // Assert
        Assert.IsNotNull(encryptionService);
        Assert.IsInstanceOfType(encryptionService, typeof(PasswordEncryption));
    }
}
