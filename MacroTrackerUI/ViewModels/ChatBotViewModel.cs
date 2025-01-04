using MacroTrackerCore.Services.ChatBotService;
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using Microsoft.Extensions.DependencyInjection;
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
    /// Gets or sets the service provider.
    /// </summary>
    public IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Gets or sets the content of the prompt.
    /// </summary>
    public string PromptContent { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ChatBot instance.
    /// </summary>
    public IChatBot ChatBot { get; set; }

    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatBotViewModel"/> class.
    /// </summary>
    public ChatBotViewModel()
    {
        ServiceProvider = ProviderUI.GetServiceProvider();
        ChatBot = ServiceProvider.GetService<IChatBot>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatBotViewModel"/> class with a specified service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public ChatBotViewModel(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        ChatBot = ServiceProvider.GetService<IChatBot>();
    }

    /// <summary>
    /// Sends the user prompt to the ChatBot and handles the response.
    /// </summary>
    public async void SendPrompt()
    {
        AddUserMessage(PromptContent);
        AddAssistantMessage("Thinking...");

        try
        {
            string response = await RunWithTimeout(ChatBot.GetResponse(PromptContent), TimeSpan.FromSeconds(30));
            ReplaceLastAssistantMessage(response);
        }
        catch (Exception)
        {
            ReplaceLastAssistantMessage("There is an error.", Message.RoleType.AssistantError);
        }
    }

    /// <summary>
    /// Runs a task with a specified timeout.
    /// </summary>
    /// <param name="task">The task to run.</param>
    /// <param name="timeout">The timeout duration.</param>
    /// <returns>The result of the task if it completes within the timeout; otherwise, throws a <see cref="TimeoutException"/>.</returns>
    private static async Task<string> RunWithTimeout(Task<string> task, TimeSpan timeout)
    {
        if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
        {
            return await task;
        }
        else
        {
            throw new TimeoutException();
        }
    }

    /// <summary>
    /// Adds a user message to the conversation.
    /// </summary>
    /// <param name="content">The content of the message.</param>
    private void AddUserMessage(string content)
    {
        App.ChatBotConversation.Add(new Message
        {
            Content = content,
            Role = Message.RoleType.User
        });
    }

    /// <summary>
    /// Adds an assistant message to the conversation.
    /// </summary>
    /// <param name="content">The content of the message.</param>
    private void AddAssistantMessage(string content)
    {
        App.ChatBotConversation.Add(new Message
        {
            Content = content,
            Role = Message.RoleType.Assistant
        });
    }

    /// <summary>
    /// Replaces the last assistant message in the conversation.
    /// </summary>
    /// <param name="content">The new content of the message.</param>
    /// <param name="role">The role of the message.</param>
    private void ReplaceLastAssistantMessage(string content, Message.RoleType role = Message.RoleType.Assistant)
    {
        App.ChatBotConversation.RemoveAt(App.ChatBotConversation.Count - 1);
        App.ChatBotConversation.Add(new Message
        {
            Content = content,
            Role = role
        });
    }
}
