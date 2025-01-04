using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? EncryptedPassword { get; set; }

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
