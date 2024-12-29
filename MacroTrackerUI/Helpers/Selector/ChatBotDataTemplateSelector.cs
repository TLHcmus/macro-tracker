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
            switch (message.Role)
            {
                case Message.RoleType.User:
                    return UserTemplate;
                case Message.RoleType.UserError:
                    return UserErrorTemplate;
                case Message.RoleType.Assistant:
                    return BotTemplate;
                case Message.RoleType.AssistantError:
                    return AssistantErrorTemplate;
            }
        }

        return base.SelectTemplateCore(item, container);
    }
}
