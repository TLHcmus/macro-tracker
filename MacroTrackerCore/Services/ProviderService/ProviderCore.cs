using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using Microsoft.Extensions.DependencyInjection;

namespace MacroTrackerCore.Services.ProviderService;

/// <summary>
/// Provides core services for dependency injection.
/// </summary>
public static class ProviderCore
{
    private static readonly ServiceProvider serviceProvider = SetUpDependencyInjection();

    /// <summary>
    /// Sets up the dependency injection container and registers services.
    /// </summary>
    /// <returns>A configured <see cref="ServiceProvider"/>.</returns>
    private static ServiceProvider SetUpDependencyInjection()
    {
        ServiceCollection services = new();
        services.AddSingleton<IDao, DatabaseDao>();
        services.AddSingleton<IPasswordEncryption, PasswordEncryption>();

        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Gets the configured <see cref="ServiceProvider"/>.
    /// </summary>
    /// <returns>The <see cref="ServiceProvider"/> instance.</returns>
    public static ServiceProvider GetServiceProvider() => serviceProvider;
}
