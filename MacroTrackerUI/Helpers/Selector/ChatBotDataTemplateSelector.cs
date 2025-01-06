using MacroTrackerUI.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MacroTrackerUI.Helpers.Selector;

/// <summary>
/// Selects the appropriate DataTemplate based on the role of the message.
/// </summary>
public class ChatBotDataTemplateSelector : DataTemplateSelector
{
    /// <summary>
    /// Gets or sets the DataTemplate for user messages.
    /// </summary>
    public DataTemplate UserTemplate { get; set; }

    /// <summary>
    /// Gets or sets the DataTemplate for bot messages.
    /// </summary>
    public DataTemplate BotTemplate { get; set; }

    /// <summary>
    /// Gets or sets the DataTemplate for user error messages.
    /// </summary>
    public DataTemplate UserErrorTemplate { get; set; }

    /// <summary>
    /// Gets or sets the DataTemplate for assistant error messages.
    /// </summary>
    public DataTemplate AssistantErrorTemplate { get; set; }

    /// <summary>
    /// Selects the appropriate DataTemplate based on the role of the message.
    /// </summary>
    /// <param name="item">The data object for which to select the template.</param>
    /// <param name="container">The data-bound object.</param>
    /// <returns>The selected DataTemplate or the base implementation if no match is found.</returns>
    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
    {
        if (item is Message message)
        {
            return SelectTemplateChatBot(message) ?? base.SelectTemplateCore(item, container);
        }

        return base.SelectTemplateCore(item, container);
    }

    /// <summary>
    /// Selects the appropriate DataTemplate based on the role of the message.
    /// </summary>
    /// <param name="message">The message object containing the role type.</param>
    /// <returns>The selected DataTemplate or null if no match is found.</returns>
    public DataTemplate SelectTemplateChatBot(Message message)
    {
        return message.Role switch
        {
            Message.RoleType.User => UserTemplate,
            Message.RoleType.UserError => UserErrorTemplate,
            Message.RoleType.Assistant => BotTemplate,
            Message.RoleType.AssistantError => AssistantErrorTemplate,
            _ => null,
        };
    }
}
