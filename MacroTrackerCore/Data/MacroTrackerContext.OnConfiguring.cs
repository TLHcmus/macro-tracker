using DotEnv.Core;
using MacroTrackerCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MacroTrackerCore.Data;

/// <summary>
/// Represents the database context for the MacroTracker application.
/// </summary>
public partial class MacroTrackerContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MacroTrackerContext"/> class.
    /// </summary>
    public MacroTrackerContext() { }

    /// <summary>
    /// Configures the database context options.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to be configured.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            new EnvLoader().Load();
            var envVars = new EnvReader();

            string connectionString = $"""
                server={envVars["DB_HOST"]};
                port={envVars["DB_PORT"]};
                database={envVars["DB_NAME"]};
                user={envVars["DB_USER"]};
                password={envVars["DB_PASSWORD"]};
             """;

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}
