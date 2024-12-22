using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using Microsoft.Extensions.DependencyInjection;

namespace MacroTrackerCore.Services.ProviderService
{
    public static class ProviderCore
    {
        private static readonly ServiceProvider serviceProvider = SetUpDependencyInjection();

        private static ServiceProvider SetUpDependencyInjection()
        {
            ServiceCollection services = new();
            services.AddSingleton<IDao, DatabaseDao>();
            //services.AddSingleton<IDao, MockDao>();
            services.AddSingleton<IPasswordEncryption, PasswordEncryption>();

            return services.BuildServiceProvider();
        }

        public static ServiceProvider GetServiceProvider() => serviceProvider;
    }
}
