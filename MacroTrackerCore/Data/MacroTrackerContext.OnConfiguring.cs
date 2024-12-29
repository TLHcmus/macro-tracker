using DotEnv.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MacroTrackerCore.Data;

/// <summary>
/// Represents the database context for the MacroTracker application.
/// </summary>
public partial class MacroTrackerContext : DbContext
{
    public string InitPath { get; private set; } = "..\\Properties\\DatabaseSetup\\Init\\init_sqlite.sql";

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

            if (envVars["ENV"] == "development")
            {
                Debug.WriteLine($"Development Database.");
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
            }
            else if (envVars["ENV"] == "testing")
            {
                Debug.WriteLine("Testing Database.");
                optionsBuilder.UseSqlite("Data Source=:memory:").LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
            }
        }
    }

    public void GenerateSchemaFromFileForTest(Action testMethod)
    {
        Database.EnsureCreated();

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, InitPath);

        // Check if the file exists
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Schema SQL file not found: {path}");
        }

        // Read SQL commands
        var sqlCommands = File.ReadAllText(path);

        // Execute SQL commands
        var connection = this.Database.GetDbConnection();
        connection.Open();
        using (var transaction = connection.BeginTransaction())
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = sqlCommands;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
        }

        // Run the test method
        testMethod();

        // Clean up
        connection.Close();
    }
}
