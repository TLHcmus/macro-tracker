using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using MacroTracker.ViewModel;
using MacroTracker.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTracker.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        LoginViewModel ViewModel { get; } = new LoginViewModel();

        Frame RootFrame { get; set; }

        Frame LoginShellFrame {  get; set; }
        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Login button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Username = UsernameBox.Text;
            ViewModel.Password = PasswordEncryptionHelper.EncryptPasswordToDatabase(PasswordBox.Password);

            if (ViewModel.DoesUserMatchPassword())
                RootFrame.Navigate(typeof(MainPage));
            else
            {
                var contentDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Content = "Invalid Login",
                    CloseButtonText = "OK",
                };

                await contentDialog.ShowAsync();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var pairFrame = e.Parameter as (Frame, Frame)?;
            if (pairFrame.HasValue)
            {
                RootFrame = pairFrame.Value.Item1 as Frame;
                LoginShellFrame = pairFrame.Value.Item2 as Frame;
            }
            else throw new Exception("Cannot cast to Frame");
        }
    }
}
