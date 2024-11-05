using MacroTracker.Service;
using MacroTracker.Service.DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.ViewModel
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        private IDao Dao { get; } = ServiceRegistry.RegisteredService["IDao"] as IDao;
        /// <summary>
        /// Check if username and password match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool DoesUserMatchPassword()
        {
            if (Dao.DoesUserMatchPassword(Username, Password))
                return true;
            return false;
        }
    }
}
