using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using DotEnv.Core;
using System.Diagnostics;

namespace MacroTrackerCore.Services.ChatBotService;

/// <summary>
/// Represents a chatbot service for macro tracking and related queries.
/// </summary>
public class ChatBot
{
    /// <summary>
    /// Gets the Kernel instance used by the chatbot.
    /// </summary>
    public Kernel Kernel { get; private set; }

    /// <summary>
    /// Gets the chat completion service used by the chatbot.
    /// </summary>
    public IChatCompletionService ChatCompletionService { get; private set; }

    /// <summary>
    /// Gets the prompt execution settings for the chatbot.
    /// </summary>
    public PromptExecutionSettings PromptExecutionSettings { get; private set; }

    /// <summary>
    /// Gets the chat history of the chatbot.
    /// </summary>
    public ChatHistory History { get; private set; } = new ChatHistory();

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatBot"/> class.
    /// </summary>
    public ChatBot()
    {
        new EnvLoader().Load();
        var envVars = new EnvReader();

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var builder = Kernel.CreateBuilder().AddHuggingFaceChatCompletion(
            model: "Qwen/QwQ-32B-Preview",
            apiKey: envVars["CHATBOT_API_KEY"]
        );

        Kernel = builder.Build();
        ChatCompletionService = Kernel.GetRequiredService<IChatCompletionService>();

        PromptExecutionSettings = new HuggingFacePromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
            MaxTokens = 500
        };
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    }

    /// <summary>
    /// Gets the response from the chatbot for a given prompt.
    /// </summary>
    /// <param name="prompt">The user prompt.</param>
    /// <returns>The chatbot's response.</returns>
    public async Task<string> GetResponse(string prompt)
    {
        // Add system message
        History.AddSystemMessage("""
            You are a professional macro tracker.
            You only answer the questions related to nutritions, calories, food, health and fitness.
            You can have some fun and casual conversations with the user, but you must always stay professional.
            And you must deny all unrelated questions at all costs.
        """);

        History.AddUserMessage(prompt);

        try
        {
            ChatMessageContent response = await ChatCompletionService.GetChatMessageContentAsync(History, PromptExecutionSettings);
            if (response.Content != null)
            {
                History.AddMessage(response.Role, response.Content);
            }
            return response.Content ?? "";
        }
        catch (Exception)
        {
            throw;
        }
    }
}
