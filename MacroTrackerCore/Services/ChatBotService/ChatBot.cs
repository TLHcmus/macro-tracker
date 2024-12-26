using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using DotEnv.Core;
using System.Diagnostics;

namespace MacroTrackerCore.Services.ChatBotService;

public class ChatBot
{
    public Kernel Kernel { get; private set; }
    public IChatCompletionService ChatCompletionService { get; private set; }
    public PromptExecutionSettings PromptExecutionSettings { get; private set; }
    public ChatHistory History { get; private set; } = [];

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
