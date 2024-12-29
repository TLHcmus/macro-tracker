using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class Message
{
    public string Content { get; set; }
    public RoleType Role { get; set; }
    public enum RoleType { User, Assistant, UserError, AssistantError }
}
