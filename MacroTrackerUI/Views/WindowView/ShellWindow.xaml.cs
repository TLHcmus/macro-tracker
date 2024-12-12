using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;

namespace MacroTrackerUI.Views.WindowView
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(MainPage));
        }
    }
}
