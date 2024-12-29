using DotEnv.Core;
using MacroTrackerCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MacroTrackerCore.Data
{
    public partial class MacroTrackerContext : DbContext
    {
        public MacroTrackerContext() { }
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
}
