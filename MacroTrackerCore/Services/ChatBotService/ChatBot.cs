using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using DotEnv.Core;

namespace MacroTrackerCore.Services.ChatBotService;

/// <summary>
/// Represents a chatbot service for macro tracking and related queries.
/// </summary>
public class ChatBot : IChatBot
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
        InitializeEnvironment();
        InitializeKernel();
    }

    /// <summary>
    /// Initializes environment variables.
    /// </summary>
    private void InitializeEnvironment() => new EnvLoader().Load();

    /// <summary>
    /// Initializes the Kernel and related services.
    /// </summary>
    private void InitializeKernel()
    {
        var envVars = new EnvReader();
#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var builder = Kernel.CreateBuilder().AddHuggingFaceChatCompletion(
            model: "Qwen/QwQ-32B-Preview",
            apiKey: envVars["CHATBOT_API_KEY"]
        );

        Kernel = builder.Build();
        ChatCompletionService = Kernel.GetRequiredService<IChatCompletionService>();
        PromptExecutionSettings = new HuggingFacePromptExecutionSettings
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
        AddSystemMessage();
        History.AddUserMessage(prompt);

        try
        {
            var response = await ChatCompletionService.GetChatMessageContentAsync(History, PromptExecutionSettings);
            if (response.Content != null)
            {
                History.AddMessage(response.Role, response.Content);
            }
            return response.Content ?? string.Empty;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Adds the system message to the chat history.
    /// </summary>
    private void AddSystemMessage()
    {
        History.AddSystemMessage("""
            You are a professional macro tracker.
            You only answer the questions related to nutritions, calories, food, health, exercises fitness.
            You can have some fun and casual conversations with the user, but you must always stay professional.
            And you should gentlely deny all unrelated questions.
        """);
    }
}
