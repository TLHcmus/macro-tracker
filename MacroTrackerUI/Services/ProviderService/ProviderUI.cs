using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;

namespace MacroTrackerUI.Services.ProviderService
{
    /// <summary>
    /// Provides a service provider for dependency injection.
    /// </summary>
    public static class ProviderUI
    {
        /// <summary>
        /// The configured service provider.
        /// </summary>
        private static readonly ServiceProvider serviceProvider = SetUpDependencyInjection();

        /// <summary>
        /// Sets up the dependency injection by registering services.
        /// </summary>
        /// <returns>A configured <see cref="ServiceProvider"/>.</returns>
        private static ServiceProvider SetUpDependencyInjection()
        {
            ServiceCollection services = new();
            services.AddSingleton<PasswordEncryptionReceiver>();
            services.AddSingleton<DaoReceiver>();

            services.AddSingleton<PasswordEncryptionSender>();
            services.AddSingleton<DaoSender>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets the configured service provider.
        /// </summary>
        /// <returns>The configured <see cref="ServiceProvider"/>.</returns>
        public static ServiceProvider GetServiceProvider() => serviceProvider;
    }
}
