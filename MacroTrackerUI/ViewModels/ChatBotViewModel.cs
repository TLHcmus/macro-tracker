using MacroTrackerCore.Services.ChatBotService;
using MacroTrackerUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for the ChatBot interaction.
/// </summary>
public class ChatBotViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the content of the prompt.
    /// </summary>
    public string PromptContent { get; set; } = "";

    /// <summary>
    /// Gets or sets the ChatBot instance.
    /// </summary>
    public ChatBot ChatBot { get; set; } = new();

    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Sends the user prompt to the ChatBot and handles the response.
    /// </summary>
    public async void SendPrompt()
    {
        App.ChatBotConversation.Add(new Message
        {
            Content = PromptContent,
            Role = Message.RoleType.User
        });

        App.ChatBotConversation.Add(new Message
        {
            Content = "Thinking...",
            Role = Message.RoleType.Assistant
        });

        try
        {
            string response = await ChatBot.GetResponse(PromptContent);

            App.ChatBotConversation.RemoveAt(App.ChatBotConversation.Count - 1);

            App.ChatBotConversation.Add(new Message
            {
                Content = response,
                Role = Message.RoleType.Assistant
            });
        }
        catch (Exception)
        {
            App.ChatBotConversation.RemoveAt(App.ChatBotConversation.Count - 1);

            App.ChatBotConversation.Add(new Message
            {
                Content = "",
                Role = Message.RoleType.AssistantError
            });
        }
    }
}
