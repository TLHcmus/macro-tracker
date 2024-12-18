using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for handling user sign-up functionality.
/// </summary>
public class SignUpViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the username entered by the user.
    /// </summary>
    public string Username { get; set; } = "";

    /// <summary>
    /// Gets or sets the password entered by the user.
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    /// Gets or sets the re-entered password for confirmation.
    /// </summary>
    public string ReenteredPassword { get; set; } = "";

    /// <summary>
    /// Gets the data access object for sending data.
    /// </summary>
    private DaoSender Dao { get; } = ProviderUI.GetServiceProvider().GetService<DaoSender>();

    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Checks if the sign-up is valid. This includes:
    /// - Checking if the username is null or empty.
    /// - Checking if the username already exists.
    /// - Checking if the password is strong.
    /// - Checking if the passwords match.
    /// If everything is valid, adds the user to the database and assigns a successful prompt message.
    /// If the username is null or empty, does nothing and the prompt message is null.
    /// Otherwise, assigns an error prompt message.
    /// </summary>
    /// <param name="promptMessage">The prompt message to be assigned based on the validation result.</param>
    /// <returns>
    /// true: if the sign-up is valid with the prompt message: "Sign up successfully".
    /// false: if the sign-up is invalid with one of these prompt messages: "Username has already existed!", "Password is not strong!", "Passwords do not match!".
    /// null: no action is taken and the prompt message is not assigned.
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
            promptMessage = "Username has already existed!";
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
    /// Checks if the password is strong with a length of at least 8 characters.
    /// </summary>
    /// <returns>true if the password is strong; otherwise, false.</returns>
    public bool IsPasswordStrong()
    {
        if (Password == null || Password.Length < 8)
            return false;
        return true;
    }

    /// <summary>
    /// Checks if the username already exists.
    /// </summary>
    /// <returns>true if the username exists; otherwise, false.</returns>
    public bool DoesUsernameExist()
    {
        if (Username == null || Username == "" || Dao.DoesUsernameExist(Username))
            return true;
        return false;
    }

    /// <summary>
    /// Checks if the passwords match.
    /// </summary>
    /// <returns>true if the passwords match; otherwise, false.</returns>
    public bool DoPasswordsMatch()
    {
        if (Password == ReenteredPassword)
            return true;
        return false;
    }

    /// <summary>
    /// Gets the password encryption handler.
    /// </summary>
    public PasswordEncryptionSender PasswordEncryptionHandle { get; } =
        ProviderUI.GetServiceProvider().GetService<PasswordEncryptionSender>();

    /// <summary>
    /// Adds a user to the database.
    /// </summary>
    public void AddUser()
    {
        // Ma hoa mat khau
        string encryptedPassword = PasswordEncryptionHandle.EncryptPasswordToDatabase(Password);

        Dao.AddUser((Username, Password));
    }
}
