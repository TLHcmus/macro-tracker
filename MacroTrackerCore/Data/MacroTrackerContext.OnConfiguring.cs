using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace MacroTrackerCore.Data;

/// <summary>
/// Represents the database context for the MacroTracker application.
/// </summary>
public partial class MacroTrackerContext : DbContext
{
    public string InitPathForTest { get; private set; } = "Properties\\DatabaseSetup\\init_sqlite.sql";
    public string Env { get; private set; } = "dev";

    /// <summary>
    /// Initializes a new instance of the <see cref="MacroTrackerContext"/> class.
    /// </summary>
    public MacroTrackerContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MacroTrackerContext"/> class with a specified environment.
    /// </summary>
    /// <param name="env">The environment to be used.</param>
    public MacroTrackerContext(string env)
    {
        Env = env;
    }

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

            if (Env == "dev")
            {
                Debug.WriteLine("Development Database.");
                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
            }
            else if (Env == "test")
            {
                Debug.WriteLine("Testing Database.");
                optionsBuilder.UseSqlite("Data Source=:memory:");
            }
        }
    }

    /// <summary>
    /// Gets the initialization path for the SQLite database used in testing.
    /// </summary>
    /// <returns>The path to the initialization SQL file.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the schema SQL file is not found.</exception>
    public string GetInitSqlitePathForTest()
    {
        Database.EnsureCreated();
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, InitPathForTest);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Schema SQL file not found: {path}");
        }

        return path;
    }

    /// <summary>
    /// Initializes the SQLite database for testing.
    /// </summary>
    /// <returns>The database connection.</returns>
    public DbConnection InitSqliteForTest()
    {
        var path = GetInitSqlitePathForTest();
        var sqlCommands = File.ReadAllText(path);

        var connection = Database.GetDbConnection();
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

        return connection;
    }

    /// <summary>
    /// Disposes the SQLite database connection used in testing.
    /// </summary>
    /// <param name="connection">The database connection to be disposed.</param>
    public void DisposeSqliteForTest(DbConnection connection)
    {
        if (connection != null && connection.State != ConnectionState.Closed)
        {
            connection.Close();
        }
    }
}
