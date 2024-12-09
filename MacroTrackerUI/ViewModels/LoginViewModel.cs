using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string DatabaseEncryptedPassword { get; set; }

        public DaoSender Dao { get; } =
            ProviderUI.GetServiceProvider().GetService<DaoSender>();

        public PasswordEncryptionSender PasswordEncryptionSender { get; } =
            ProviderUI.GetServiceProvider().GetService<PasswordEncryptionSender>();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Check if username and database encrypted password match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool DoesUserMatchPassword()
        {
            if (Dao.DoesUserMatchPassword(Username, DatabaseEncryptedPassword))
                return true;
            return false;
        }
    }
}
