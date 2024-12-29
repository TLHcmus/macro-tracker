using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

/// <summary>
/// Represents a message with content and role type.
/// </summary>
public class Message
{
    /// <summary>
    /// Gets or sets the content of the message.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the role type of the message.
    /// </summary>
    public RoleType Role { get; set; }

    /// <summary>
    /// Enum representing the role type of the message.
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// Represents a user role.
        /// </summary>
        User,

        /// <summary>
        /// Represents an assistant role.
        /// </summary>
        Assistant,

        /// <summary>
        /// Represents a user error role.
        /// </summary>
        UserError,

        /// <summary>
        /// Represents an assistant error role.
        /// </summary>
        AssistantError
    }
}
