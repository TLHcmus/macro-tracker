using MacroTrackerCore.Data;
using MacroTrackerCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MacroTrackerCoreTest.Data;

[TestClass]
public sealed class MacroTrackerContextIT
{
    [TestClass]
    public class DatabaseConnectionIT
    {
        [TestMethod]
        public void OnConfiguring_WithTestEnvironment_ContextIsInitialized()
        {
            // Arrange
            MacroTrackerContext context = new("test");

            // Act
            context.Database.EnsureCreated();

            // Assert
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void GetInitSqlitePathForTest_WhenCalled_ReturnsValidPath()
        {
            // Arrange
            MacroTrackerContext context = new("test");

            // Act
            context.Database.EnsureCreated();
            string initSqlitePath = context.GetInitSqlitePathForTest();

            // Assert
            Assert.IsNotNull(initSqlitePath);
        }

        [TestMethod]
        public void OnConfiguring_WithDevEnvironment_UsesMySql()
        {
            // Arrange
            MacroTrackerContext context = new("dev");

            // Act & Assert
            try
            {
                context.Database.EnsureCreated();
            }
            catch (InvalidOperationException)
            {
                Assert.Fail("Expected no exception, but got InvalidOperationException.");
            }
        }

        [TestMethod]
        public void OnModelCreating_SelectAllUsersWithDevEnvironment_ReturnsAllUsers()
        {
            // Arrange
            MacroTrackerContext context = new("dev");

            // Act
            var users = context.Users.ToList();
            var admin = users.FirstOrDefault(u => u.Username == "admin");

            // Assert
            Assert.IsNotNull(users);
            Assert.IsNotNull(admin);
        }

        [TestMethod]
        public void OnModelCreating_WhenCalled_ConfiguresExerciseEntity()
        {
            // Arrange
            MacroTrackerContext context = new("test");

            // Act
            var exerciseEntity = context.Model.FindEntityType(typeof(MacroTrackerCore.Entities.Exercise));

            // Assert
            Assert.IsNotNull(exerciseEntity);
            Assert.AreEqual("exercises", exerciseEntity.GetTableName());
        }

        [TestMethod]
        public void OnModelCreating_WhenCalled_ConfiguresFoodEntity()
        {
            // Arrange
            MacroTrackerContext context = new("test");

            // Act
            var foodEntity = context.Model.FindEntityType(typeof(MacroTrackerCore.Entities.Food));

            // Assert
            Assert.IsNotNull(foodEntity);
            Assert.AreEqual("foods", foodEntity.GetTableName());
        }

        [TestMethod]
        public void OnModelCreating_WhenCalled_ConfiguresUserEntity()
        {
            // Arrange
            MacroTrackerContext context = new("test");

            // Act
            var userEntity = context.Model.FindEntityType(typeof(MacroTrackerCore.Entities.User));

            // Assert
            Assert.IsNotNull(userEntity);
            Assert.AreEqual("users", userEntity.GetTableName());
        }

        [TestMethod]
        public void GetInitSqlitePathForTest_WithExistingFilePath_ReturnsCorrectPath()
        {
            // Arrange
            MacroTrackerContext context = new("test");

            // Act
            string path = context.GetInitSqlitePathForTest();

            // Assert
            Assert.IsTrue(File.Exists(path));
        }

        [TestMethod]
        public void OnConfiguring_WithDefaultEnvironment_UsesMySql()
        {
            // Arrange
            MacroTrackerContext context = new();

            // Act & Assert
            try
            {
                context.Database.EnsureCreated();
            }
            catch (InvalidOperationException)
            {
                Assert.Fail("Expected no exception, but got InvalidOperationException.");
            }
        }
    }

    [TestClass]
    public class UserEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    [TestClass]
    public class GoalEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    [TestClass]
    public class ExerciseEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    [TestClass]
    public class FoodEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    [TestClass]
    public class LogExerciseItemEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    [TestClass]
    public class LogFoodItemEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

    [TestClass]
    public class LogEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}