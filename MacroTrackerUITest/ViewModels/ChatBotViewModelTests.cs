using MacroTrackerCore.Services.ChatBotService;
using MacroTrackerUI;
using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class ChatBotViewModelTests
{
    private Mock<IChatBot> _chatBotMock;
    private IServiceProvider _serviceProvider;

    [TestInitialize]
    public void Setup()
    {
        _chatBotMock = new Mock<IChatBot>();

        var serviceMock = new Mock<IServiceProvider>();
        serviceMock.Setup(
            s => s.GetService(typeof(IChatBot))
        ).Returns(_chatBotMock.Object);

        _serviceProvider = serviceMock.Object;
    }

    [TestMethod]
    public async Task SendPrompt_ShouldAddUserMessage()
    {
        // Arrange
        var viewModel = new ChatBotViewModel(_serviceProvider)
        {
            PromptContent = "Hello, ChatBot!"
        };
        App.ChatBotConversation = [];

        // Act
        viewModel.SendPrompt();
        await Task.Delay(100); // Allow some time for async operation

        // Assert
        Assert.AreEqual(2, App.ChatBotConversation.Count);
        Assert.AreEqual("Hello, ChatBot!", App.ChatBotConversation[0].Content);
        Assert.AreEqual(Message.RoleType.User, App.ChatBotConversation[0].Role);
        Assert.IsNull(App.ChatBotConversation[1].Content);
        Assert.AreEqual(Message.RoleType.Assistant, App.ChatBotConversation[1].Role);
    }

    [TestMethod]
    public async Task SendPrompt_ShouldAddAssistantResponse()
    {
        // Arrange
        _chatBotMock.Setup(
            cb => cb.GetResponse(It.IsAny<string>())
        ).ReturnsAsync("Response from ChatBot");
        var viewModel = new ChatBotViewModel(_serviceProvider)
        {
            PromptContent = "Hello, ChatBot!"
        };
        App.ChatBotConversation = [];

        // Act
        viewModel.SendPrompt();

        await Task.Delay(100); // Allow some time for async operation

        // Assert
        Assert.AreEqual(2, App.ChatBotConversation.Count);
        Assert.AreEqual("Response from ChatBot", App.ChatBotConversation[1].Content);
        Assert.AreEqual(Message.RoleType.Assistant, App.ChatBotConversation[1].Role);
    }

    [TestMethod]
    public async Task SendPrompt_ShouldHandleTimeout()
    {
        // Arrange
        _chatBotMock.Setup(cb => cb.GetResponse(It.IsAny<string>())).Returns(async () =>
        {
            await Task.Delay(32000); // Simulate long response time
            return "Response from ChatBot";
        });
        var viewModel = new ChatBotViewModel(_serviceProvider)
        {
            PromptContent = "Hello, ChatBot!"
        };
        App.ChatBotConversation = [];

        // Act
        viewModel.SendPrompt();
        await Task.Delay(32000); // Allow some time for async operation

        // Assert
        Assert.AreEqual(2, App.ChatBotConversation.Count);
        Assert.IsTrue(App.ChatBotConversation[1].Content.Contains("error"));
        Assert.AreEqual(Message.RoleType.AssistantError, App.ChatBotConversation[1].Role);
    }
}
