using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using System.Collections.ObjectModel;
using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;

namespace MacroTrackerUI.Views.UserControlView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatBot : Page
    {
        public ChatBotViewModel ViewModel { get; set; } = new();

        public static readonly DependencyProperty ChatBotConversationProperty =
            DependencyProperty.Register("ChatBotConversation", 
                                        typeof(ObservableCollection<Message>), 
                                        typeof(ChatBot), 
                                        new PropertyMetadata(new ObservableCollection<Message>()));

        public ObservableCollection<Message> ChatBotConversation
        {
            get { return (ObservableCollection<Message>)GetValue(ChatBotConversationProperty); }
            set { SetValue(ChatBotConversationProperty, value); }
        }

        public ChatBot()
        {
            this.InitializeComponent();
            MoveUpDownAnimation.Begin(); // Start the animation
        }

        private void ChatBotIcon_Click(object sender, RoutedEventArgs e)
        {
            ChatBotIcon.Visibility = Visibility.Collapsed;
            Conversation.Visibility = Visibility.Visible;
        }

        private void ConversationClose_Click(object sender, RoutedEventArgs e)
        {
            ChatBotIcon.Visibility = Visibility.Visible;
            Conversation.Visibility = Visibility.Collapsed;
        }

        private HashSet<VirtualKey> KeyList { get; set; } = new HashSet<VirtualKey>();

        private void Key_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            KeyList.Add(e.Key);

            // Check if the Enter key is pressed (without combinations)
            if (e.Key == VirtualKey.Enter)
            {
                if (ViewModel.PromptContent != "" && ViewModel.PromptContent != null && KeyList.Count == 1)
                {
                    ViewModel.SendPrompt();
                    ViewModel.PromptContent = "";
                }

                if (KeyList.Count == 2 && (KeyList.Contains(VirtualKey.LeftControl) || KeyList.Contains(VirtualKey.RightControl) || KeyList.Contains(VirtualKey.Control)))
                {
                    ViewModel.PromptContent += "\n";
                    PromptFill.SelectionStart = ViewModel.PromptContent.Length;
                }

            }
        }

        private void Key_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            KeyList.Remove(e.Key);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.PromptContent != "" && ViewModel.PromptContent != null)
            {
                ViewModel.SendPrompt();
                ViewModel.PromptContent = "";
            }
        }
    }
}
