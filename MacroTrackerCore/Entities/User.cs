namespace MacroTrackerCore.Entities;

/// <summary>
/// Represents a user in the MacroTracker system.
/// </summary>
public partial class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the encrypted password of the user.
    /// </summary>
    public string? EncryptedPassword { get; set; }

    /// <summary>
    /// Gets or sets the collection of goals associated with the user.
    /// </summary>
    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    /// <summary>
    /// Gets or sets the collection of logs associated with the user.
    /// </summary>
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
