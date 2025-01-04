using Microsoft.SemanticKernel.ChatCompletion;

using Microsoft.SemanticKernel;

namespace MacroTrackerCore.Services.ChatBotService;

/// <summary>
/// Represents the interface for a chatbot service for macro tracking and related queries.
/// </summary>
public interface IChatBot
{
    /// <summary>
    /// Gets the Kernel instance used by the chatbot.
    /// </summary>
    Kernel Kernel { get; }

    /// <summary>
    /// Gets the chat completion service used by the chatbot.
    /// </summary>
    IChatCompletionService ChatCompletionService { get; }

    /// <summary>
    /// Gets the prompt execution settings for the chatbot.
    /// </summary>
    PromptExecutionSettings PromptExecutionSettings { get; }

    /// <summary>
    /// Gets the chat history of the chatbot.
    /// </summary>
    ChatHistory History { get; }

    /// <summary>
    /// Gets the response from the chatbot for a given prompt.
    /// </summary>
    /// <param name="prompt">The user prompt.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the chatbot's response.</returns>
    Task<string> GetResponse(string prompt);
}
