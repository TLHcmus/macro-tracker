using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using System.Collections.ObjectModel;
using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;

namespace MacroTrackerUI.Views.UserControlView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ChatBot : Page
{
    /// <summary>
    /// Gets or sets the ViewModel for the ChatBot interaction.
    /// </summary>
    public ChatBotViewModel ViewModel { get; set; } = new();

    /// <summary>
    /// Identifies the ChatBotConversation dependency property.
    /// </summary>
    public static readonly DependencyProperty ChatBotConversationProperty =
        DependencyProperty.Register("ChatBotConversation",
                                    typeof(ObservableCollection<Message>),
                                    typeof(ChatBot),
                                    new PropertyMetadata(new ObservableCollection<Message>()));

    /// <summary>
    /// Gets or sets the conversation of the ChatBot.
    /// </summary>
    public ObservableCollection<Message> ChatBotConversation
    {
        get { return (ObservableCollection<Message>)GetValue(ChatBotConversationProperty); }
        set { SetValue(ChatBotConversationProperty, value); }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatBot"/> class.
    /// </summary>
    public ChatBot()
    {
        this.InitializeComponent();
        MoveUpDownAnimation.Begin(); // Start the animation
    }

    /// <summary>
    /// Handles the Click event of the ChatBotIcon control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void ChatBotIcon_Click(object sender, RoutedEventArgs e)
    {
        ChatBotIcon.Visibility = Visibility.Collapsed;
        Conversation.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Handles the Click event of the ConversationClose control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void ConversationClose_Click(object sender, RoutedEventArgs e)
    {
        ChatBotIcon.Visibility = Visibility.Visible;
        Conversation.Visibility = Visibility.Collapsed;
    }

    /// <summary>
    /// Handles the KeyDown event of the control. Check for 
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
    private void Key_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            // Check if the Enter key is pressed with Shift
            if (!string.IsNullOrEmpty(ViewModel.PromptContent) && Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread(Windows.System.VirtualKey.Shift).HasFlag(Windows.UI.Core.CoreVirtualKeyStates.Down))
            {
                ViewModel.PromptContent += "\n";
                PromptFill.SelectionStart = ViewModel.PromptContent.Length;
            }
            else 
            {
                ViewModel.SendPrompt();
                ViewModel.PromptContent = "";
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the SendButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(ViewModel.PromptContent))
        {
            ViewModel.SendPrompt();
            ViewModel.PromptContent = "";
        }
    }
}
