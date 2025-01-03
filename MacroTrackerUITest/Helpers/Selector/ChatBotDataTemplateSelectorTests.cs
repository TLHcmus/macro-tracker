using MacroTrackerUI.Helpers.Selector;
using MacroTrackerUI.Models;
using Microsoft.UI.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace MacroTrackerUITest.Helpers.Selector;

[TestClass]
public class ChatBotDataTemplateSelectorTests
{
    private ChatBotDataTemplateSelector _selector;
    private DataTemplate _userTemplate;
    private DataTemplate _botTemplate;
    private DataTemplate _userErrorTemplate;
    private DataTemplate _assistantErrorTemplate;

    [TestInitialize]
    public void Setup()
    {
        _userTemplate = new DataTemplate();
        _botTemplate = new DataTemplate();
        _userErrorTemplate = new DataTemplate();
        _assistantErrorTemplate = new DataTemplate();

        _selector = new ChatBotDataTemplateSelector
        {
            UserTemplate = _userTemplate,
            BotTemplate = _botTemplate,
            UserErrorTemplate = _userErrorTemplate,
            AssistantErrorTemplate = _assistantErrorTemplate
        };
    }

    [UITestMethod]
    public void SelectTemplateCore_UserMessage_ReturnsUserTemplate()
    {
        var message = new Message { Role = Message.RoleType.User };
        var result = _selector.SelectTemplateChatBot(message);
        Assert.AreEqual(_userTemplate, result);
    }

    [UITestMethod]
    public void SelectTemplateCore_BotMessage_ReturnsBotTemplate()
    {
        var message = new Message { Role = Message.RoleType.Assistant };
        var result = _selector.SelectTemplateChatBot(message);
        Assert.AreEqual(_botTemplate, result);
    }

    [UITestMethod]
    public void SelectTemplateCore_UserErrorMessage_ReturnsUserErrorTemplate()
    {
        var message = new Message { Role = Message.RoleType.UserError };
        var result = _selector.SelectTemplateChatBot(message);
        Assert.AreEqual(_userErrorTemplate, result);
    }

    [UITestMethod]
    public void SelectTemplateCore_AssistantErrorMessage_ReturnsAssistantErrorTemplate()
    {
        var message = new Message { Role = Message.RoleType.AssistantError };
        var result = _selector.SelectTemplateChatBot(message);
        Assert.AreEqual(_assistantErrorTemplate, result);
    }

    [UITestMethod]
    public void SelectTemplateCore_UnknownMessageRole_ReturnsBaseImplementation()
    {
        var message = new Message { Role = (Message.RoleType)999 };
        var result = _selector.SelectTemplateChatBot(message);
        Assert.IsNull(result);
    }
}
