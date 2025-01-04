namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents a user in the MacroTracker system.
/// </summary>
public partial class User
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the encrypted password of the user.
    /// </summary>
    public string? EncryptedPassword { get; set; }
}
