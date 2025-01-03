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
            string response = await RunWithTimeout(ChatBot.GetResponse(PromptContent), TimeSpan.FromSeconds(6));

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

    private static async Task<string> RunWithTimeout(Task<string> task, TimeSpan timeout)
    {
        if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
        {
            // Task completed within the timeout
            return await task; // Await the original task to propagate any exceptions
        }
        else
        {
            // Task timed out
            throw new TimeoutException();
        }
    }

    public static async Task SomeAsyncOperation()
    {
        // Simulate a long-running task
        await Task.Delay(5000); // 5 seconds delay
    }
}
