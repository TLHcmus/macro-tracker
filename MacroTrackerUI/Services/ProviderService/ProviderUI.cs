using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;

namespace MacroTrackerUI.Services.ProviderService
{
    public static class ProviderUI
    {
        private static readonly ServiceProvider serviceProvider = SetUpDependencyInjection();

        private static ServiceProvider SetUpDependencyInjection()
        {
            ServiceCollection services = new();
            services.AddSingleton<PasswordEncryptionReceiver>();
            services.AddSingleton<DaoReceiver>();

            services.AddSingleton<PasswordEncryptionSender>();
            services.AddSingleton<DaoSender>();

            return services.BuildServiceProvider();
        }

        public static ServiceProvider GetServiceProvider() => serviceProvider;
    }
}
