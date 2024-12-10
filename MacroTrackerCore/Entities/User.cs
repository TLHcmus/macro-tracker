using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class User
{
    public string Username { get; set; } = null!;

    public string EncryptedPassword { get; set; } = null!;
}
