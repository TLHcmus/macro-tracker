using MacroTrackerUI.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MacroTrackerUI.Helpers.Selector;

public class ChatBotDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate UserTemplate { get; set; }
    public DataTemplate BotTemplate { get; set; }
    public DataTemplate UserErrorTemplate { get; set; }
    public DataTemplate AssistantErrorTemplate { get; set; }

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
