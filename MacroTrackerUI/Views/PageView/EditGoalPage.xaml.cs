using MacroTrackerUI.Models;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.PageView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditGoalPage : Page
    {
        public Goal CurrentGoal { get; set; }


        public EditGoalPage()
        {
            this.InitializeComponent();
        }


        public void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentGoal.Calories = int.Parse(CaloriesInput.Text);
            int proteinPercentage = int.Parse(ProteinInput.Text);
            int fatPercentage = int.Parse(FatInput.Text);
            int carbsPercentage = int.Parse(CarbsInput.Text);

            CurrentGoal.Protein = (int)(CurrentGoal.Calories * proteinPercentage / 100 / 4);
            CurrentGoal.Fat = (int)(CurrentGoal.Calories * fatPercentage / 100 / 9);
            CurrentGoal.Carbs = (int)(CurrentGoal.Calories * carbsPercentage / 100 / 4);

            // Navigate to the goals page
            Frame.Navigate(typeof(GoalsPage), CurrentGoal);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Goal goal)
            {
                CurrentGoal = goal;
            }
        }
    }
}
