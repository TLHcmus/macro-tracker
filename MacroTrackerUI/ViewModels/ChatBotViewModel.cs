using MacroTrackerCore.Services.ChatBotService;
using MacroTrackerUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.ViewModels;

public class ChatBotViewModel : INotifyPropertyChanged
{
    public string PromptContent { get; set; } = "";
    public ChatBot ChatBot { get; set; } = new();

    public event PropertyChangedEventHandler PropertyChanged;

    public async void SendPrompt()
    {
        App.ChatBotConversation.Add(new()
            {
                Content = PromptContent,
                Role = Message.RoleType.User
            }
        );

        App.ChatBotConversation.Add(new()
            {
                Content = "Thinking...",
                Role = Message.RoleType.Assistant
            }       
        );

        try
        {
            string response = await ChatBot.GetResponse(PromptContent);

            App.ChatBotConversation.RemoveAt(App.ChatBotConversation.Count - 1);

            App.ChatBotConversation.Add(new()
                {
                    Content = response,
                    Role = Message.RoleType.Assistant
                }
            );
        }
        catch (Exception)
        {
            App.ChatBotConversation.RemoveAt(App.ChatBotConversation.Count - 1);

            App.ChatBotConversation.Add(new()
                {
                    Content = "",
                    Role = Message.RoleType.AssistantError
                }
            );
        }
    }
}
