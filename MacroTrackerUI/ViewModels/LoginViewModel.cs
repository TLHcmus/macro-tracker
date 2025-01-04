using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for handling login functionality.
/// </summary>
public class LoginViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets the data access object sender.
    /// </summary>
    public IDaoSender Dao { get; }

    public IServiceProvider Provider { get; }

    /// <summary>
    /// Gets the password encryption sender.
    /// </summary>
    public IPasswordEncryptionSender EncryptionSender { get; }

    public LoginViewModel()
    {
        Provider = ProviderUI.GetServiceProvider();
        Dao = Provider.GetService<IDaoSender>();
        EncryptionSender = Provider.GetService<IPasswordEncryptionSender>();
    }

    public LoginViewModel(IServiceProvider provider)
    {
        Provider = provider;
        Dao = Provider.GetService<IDaoSender>();
        EncryptionSender = Provider.GetService<IPasswordEncryptionSender>();
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Checks if the username and database encrypted password match.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the username and password match; otherwise, <c>false</c>.
    /// </returns>
    public bool DoesUserMatchPassword()
    {
        return Dao.DoesUserMatchPassword(Username, Password);
    }

    /// <summary>
    /// Checks if the login information is null or empty.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the username or password is null or empty; otherwise, <c>false</c>.
    /// </returns>
    public bool LoginInfoNull()
    {
        return string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password);
    }
}
