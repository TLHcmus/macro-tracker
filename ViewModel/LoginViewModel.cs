using MacroTracker.Service;
using MacroTracker.Service.DataAcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string DatabaseEncryptedPassword { get; set; }

        private IDao Dao { get; } = ServiceRegistry.RegisteredService["IDao"] as IDao;

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
