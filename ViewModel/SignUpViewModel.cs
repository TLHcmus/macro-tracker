using MacroTracker.Helpers;
using MacroTracker.Model;
using MacroTracker.Service;
using MacroTracker.Service.DataAcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.ViewModel
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public string ReenteredPassword { get; set; } = "";

        private IDao Dao { get; } = ServiceRegistry.RegisteredService["IDao"] as IDao;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Check if the sign up is valid includes: check if username is null or empty, check if username has existed, check if password is strong, check if passwords match
        /// If everything is valid, add the user to the database and assign a successful prompt message
        /// If username is null or empty, do nothing, the prompt message is null
        /// Others, assign an error prompt message
        /// </summary>
        /// <returns>
        /// true: if the sign up is valid with the prompt message: "Sign up successfully"
        /// false: if the sign up is invalid with one of these prompt messages: "Username has already existed!", "Password is not strong!", "Passwords do not match!"
        /// null: no action is taken and prompt message is not assigned
        /// </returns>
        public bool? IsSignUpValid(out string promptMessage)
        {
            if (Username == "" || Username == null)
            {
                promptMessage = null;
                return null;
            }
            
            if (DoesUsernameExist())
            {
                promptMessage = "Username has already existed";
                return false;
            }
            
            if (!IsPasswordStrong())
            {
                promptMessage = "Password is not strong!";
                return false;
            }
            
            if (!DoPasswordsMatch())
            {
                promptMessage = "Passwords do not match!";
                return false;
            }

            promptMessage = "Sign up successfully!";
            AddUser();
            return true;
        }

        /// <summary>
        /// Check if the password is strong with the length of at least 8 characters
        /// </summary>
        /// <returns></returns>
        private bool IsPasswordStrong()
        {
            if (Password == null || Password.Length < 8)
                return false;
            return true;
        }

        /// <summary>
        /// Check if the user exists
        /// </summary>
        /// <returns></returns>
        public bool DoesUsernameExist()
        {
            if (Username == null || Username == "" || Dao.DoesUsernameExist(Username))
                return true;
            return false;
        }

        /// <summary>
        /// Check if the passwords match
        /// </summary>
        /// <returns></returns>
        public bool DoPasswordsMatch()
        {
            if (Password == ReenteredPassword)
                return true;
            return false;
        }

        /// <summary>
        /// Add a user to the database
        /// </summary>
        public void AddUser()
        {
            Dao.AddUser(new User() 
                { 
                    Username = Username,
                    EncryptedPassword = PasswordEncryptionHelper.EncryptPasswordToDatabase(Password),
                }
            );
        }
    }
}
