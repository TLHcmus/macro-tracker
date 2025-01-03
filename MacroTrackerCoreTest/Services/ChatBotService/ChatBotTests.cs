using MacroTrackerCore.Services.ChatBotService;

namespace MacroTrackerCoreTest.Services.ChatBotService;

[TestClass]
public class ChatBotTests
{
    private ChatBot _chatBot;

    [TestInitialize]
    public void Setup()
    {
        _chatBot = new ChatBot();
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldReturnResponse_ForValidPrompt()
    {
        // Arrange
        string prompt = "What are the macros in an apple?";

        // Act
        string response = await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(response));
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldDenyUnrelatedQuestions()
    {
        // Arrange
        string prompt = "What is the capital of France? If you can't answer, say \"deny\"";

        // Act
        string response = await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsTrue(response.Contains("deny", StringComparison.OrdinalIgnoreCase));
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldAddMessagesToHistory()
    {
        // Arrange
        string prompt = "How many calories are in a banana? Your content must have the word \"calories\" in it.";

        // Act
        await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsTrue(_chatBot.History.Any(m => m.Content.Contains("calories", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    [ExpectedException(typeof(Microsoft.SemanticKernel.HttpOperationException))]
    public async Task GetResponse_ShouldThrowException_OnError()
    {
        // Arrange
        string prompt = null;

        // Act
        await _chatBot.GetResponse(prompt);
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldHandleLongPrompt()
    {
        // Arrange
        string prompt = new string('a', 1000);

        // Act
        string response = await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(response));
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldHandleSpecialCharacters()
    {
        // Arrange
        string prompt = "!@#$%^&*()";

        // Act
        string response = await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(response));
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldHandleNumericPrompt()
    {
        // Arrange
        string prompt = "1234567890";

        // Act
        string response = await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(response));
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldHandleMultilinePrompt()
    {
        // Arrange
        string prompt = "Line1\nLine2\nLine3";

        // Act
        string response = await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(response));
    }

    [TestMethod]
    [Timeout(6000)]
    public async Task GetResponse_ShouldHandleHtmlContent()
    {
        // Arrange
        string prompt = "<html><body>Test</body></html>";

        // Act
        string response = await _chatBot.GetResponse(prompt);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(response));
    }
}
