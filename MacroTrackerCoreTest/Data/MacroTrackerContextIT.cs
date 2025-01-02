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
        public void InitSqliteForTest_WhenCalled_ReturnsOpenDbConnection()
        {
            // Arrange
            MacroTrackerContext context = new("test");

            // Act
            DbConnection connection = context.InitSqliteForTest();

            // Assert
            Assert.IsNotNull(connection);
            Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);

            // Cleanup
            context.DisposeSqliteForTest(connection);
        }

        [TestMethod]
        public void DisposeSqliteForTest_WhenCalled_ClosesDbConnection()
        {
            // Arrange
            MacroTrackerContext context = new("test");
            DbConnection connection = context.InitSqliteForTest();

            // Act
            context.DisposeSqliteForTest(connection);

            // Assert
            Assert.AreEqual(System.Data.ConnectionState.Closed, connection.State);
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
        public void InitSqliteForTest_ExecutesSqlCommands_Successfully()
        {
            // Arrange
            MacroTrackerContext context = new("test");
            DbConnection connection = context.InitSqliteForTest();

            // Act
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='exercises';";
            var result = command.ExecuteScalar();

            // Assert
            Assert.IsNotNull(result);

            // Cleanup
            context.DisposeSqliteForTest(connection);
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

        [TestMethod]
        public void AddUser_WithValidDetails_ShouldPersistInDatabase()
        {
            // Arrange
            var user = new User { Username = "test_user", EncryptedPassword = "encrypted_pass" };

            // Act
            _context.Users.Add(user);
            _context.SaveChanges();

            var retrievedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual("test_user", retrievedUser.Username);
            Assert.AreEqual("encrypted_pass", retrievedUser.EncryptedPassword);
        }

        [TestMethod]
        public void GetUser_ByExistingUsername_ShouldReturnCorrectUser()
        {
            // Arrange & Act
            _context.Users.Add(new User { Username = "test_user", EncryptedPassword = "encrypted_pass" });
            _context.SaveChanges();

            var retrievedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual("test_user", retrievedUser.Username);
        }

        [TestMethod]
        public void UpdateUserPassword_WithValidUser_ShouldUpdateEncryptedPassword()
        {
            // Arrange
            var user = new User { Username = "test_user", EncryptedPassword = "old_pass" };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var retrievedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");
            Assert.IsNotNull(retrievedUser);

            retrievedUser.EncryptedPassword = "new_pass";
            _context.SaveChanges();

            var updatedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");

            // Assert
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("new_pass", updatedUser.EncryptedPassword);
        }

        [TestMethod]
        public void DeleteUser_ByExistingUsername_ShouldRemoveUserFromDatabase()
        {
            // Arrange
            var user = new User { Username = "test_user", EncryptedPassword = "encrypted_pass" };

            // Act
            _context.Users.Add(user);
            _context.SaveChanges();

            var retrievedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");
            Assert.IsNotNull(retrievedUser);

            _context.Users.Remove(retrievedUser);
            _context.SaveChanges();

            var deletedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");

            // Assert
            Assert.IsNull(deletedUser);
        }

        [TestMethod]
        public void AddUser_WithDuplicateUsername_ShouldThrowException()
        {
            // Arrange & Act
            _context.Users.Add(new User { Username = "test_user", EncryptedPassword = "pass1" });
            _context.SaveChanges();

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                _context.Users.Add(new User { Username = "test_user", EncryptedPassword = "pass2" });
                _context.SaveChanges();
            });
        }

        [TestMethod]
        public void GetAllUsers_WithMultipleUsers_ShouldReturnCorrectCount()
        {
            // Arrange & Act
            _context.Users.Add(new User { Username = "user1", EncryptedPassword = "pass1" });
            _context.Users.Add(new User { Username = "user2", EncryptedPassword = "pass2" });
            _context.Users.Add(new User { Username = "user3", EncryptedPassword = "pass3" });
            _context.SaveChanges();

            var users = _context.Users.ToList();

            // Assert
            Assert.AreEqual(4, users.Count);
        }

        [TestMethod]
        public void UpdateUser_EncryptedPasswordToNull_ShouldPersistNullValue()
        {
            // Arrange
            var user = new User { Username = "test_user", EncryptedPassword = "pass" };

            // Act
            _context.Users.Add(user);
            _context.SaveChanges();

            var retrievedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");
            Assert.IsNotNull(retrievedUser);

            retrievedUser.EncryptedPassword = null;
            _context.SaveChanges();

            var updatedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");

            // Assert
            Assert.IsNull(updatedUser.EncryptedPassword);
        }

        [TestMethod]
        public void AddUser_WithNullEncryptedPassword_ShouldPersistInDatabase()
        {
            // Arrange & Act
            var user = new User { Username = "test_user", EncryptedPassword = null };
            _context.Users.Add(user);
            _context.SaveChanges();

            var retrievedUser = _context.Users.FirstOrDefault(u => u.Username == "test_user");

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual("test_user", retrievedUser.Username);
            Assert.IsNull(retrievedUser.EncryptedPassword);
        }

        [TestMethod]
        public void AddUser_WithNullUsername_ShouldThrowException()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                _context.Users.Add(new User { Username = null!, EncryptedPassword = "pass" });
                _context.SaveChanges();
            });
        }
    }

    [TestClass]
    public class GoalEntityIT
    {
        private MacroTrackerContext _context;

        [TestInitialize]
        public void Setup_Context_CreatesInMemoryDatabase()
        {
            // Arrange
            _context = new("test");
            _context.InitSqliteForTest();
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup_Context_RemovesInMemoryDatabase()
        {
            // Cleanup
            _context.DisposeSqliteForTest(_context.Database.GetDbConnection());
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void AddGoal_WithValidData_ShouldPersistInDatabase()
        {
            // Arrange
            var goal = new Goal { Calories = 2000, Protein = 150, Carbs = 200, Fat = 70 };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == 2000);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.AreEqual(2000, retrievedGoal.Calories);
            Assert.AreEqual(150, retrievedGoal.Protein);
            Assert.AreEqual(200, retrievedGoal.Carbs);
            Assert.AreEqual(70, retrievedGoal.Fat);
        }

        [TestMethod]
        public void UpdateGoal_WithNewValues_ShouldPersistUpdatedValues()
        {
            // Arrange
            var goal = new Goal { Calories = 2000, Protein = 150, Carbs = 200, Fat = 70 };
            _context.Goals.Add(goal);
            _context.SaveChanges();

            // Act
            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == 2000);
            retrievedGoal.Calories = 2500;
            retrievedGoal.Protein = 180;
            retrievedGoal.Carbs = 220;
            retrievedGoal.Fat = 80;
            _context.SaveChanges();

            var updatedGoal = _context.Goals.FirstOrDefault(g => g.Calories == 2500);

            // Assert
            Assert.IsNotNull(updatedGoal);
            Assert.AreEqual(2500, updatedGoal.Calories);
            Assert.AreEqual(180, updatedGoal.Protein);
            Assert.AreEqual(220, updatedGoal.Carbs);
            Assert.AreEqual(80, updatedGoal.Fat);
        }

        [TestMethod]
        public void AddGoal_WithNullValues_ShouldPersistInDatabase()
        {
            // Arrange
            var goal = new Goal { Calories = null, Protein = null, Carbs = null, Fat = null };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == null);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.IsNull(retrievedGoal.Calories);
            Assert.IsNull(retrievedGoal.Protein);
            Assert.IsNull(retrievedGoal.Carbs);
            Assert.IsNull(retrievedGoal.Fat);
        }

        [TestMethod]
        public void GetGoals_WithMultipleEntries_ShouldReturnCorrectCount()
        {
            // Arrange
            _context.Goals.Add(new Goal { Calories = 2000, Protein = 150, Carbs = 200, Fat = 70 });
            _context.Goals.Add(new Goal { Calories = 1800, Protein = 120, Carbs = 150, Fat = 50 });
            _context.Goals.Add(new Goal { Calories = 2200, Protein = 170, Carbs = 250, Fat = 80 });
            _context.SaveChanges();

            // Act
            var goals = _context.Goals.ToList();

            // Assert
            Assert.AreEqual(3, goals.Count);
        }

        [TestMethod]
        public void DeleteGoal_ByExistingId_ShouldRemoveGoalFromDatabase()
        {
            // Arrange
            var goal = new Goal { Calories = 2000, Protein = 150, Carbs = 200, Fat = 70 };
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == 2000);

            // Act
            _context.Goals.Remove(retrievedGoal);
            _context.SaveChanges();

            var deletedGoal = _context.Goals.FirstOrDefault(g => g.Calories == 2000);

            // Assert
            Assert.IsNull(deletedGoal);
        }

        [TestMethod]
        public void UpdateGoal_WithNullValues_ShouldPersistNullValues()
        {
            // Arrange
            var goal = new Goal { Calories = 2000, Protein = 150, Carbs = 200, Fat = 70 };
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == 2000);

            // Act
            retrievedGoal.Calories = null;
            retrievedGoal.Protein = null;
            retrievedGoal.Carbs = null;
            retrievedGoal.Fat = null;
            _context.SaveChanges();

            var updatedGoal = _context.Goals.FirstOrDefault(g => g.Calories == null);

            // Assert
            Assert.IsNotNull(updatedGoal);
            Assert.IsNull(updatedGoal.Calories);
            Assert.IsNull(updatedGoal.Protein);
            Assert.IsNull(updatedGoal.Carbs);
            Assert.IsNull(updatedGoal.Fat);
        }

        [TestMethod]
        public void AddGoal_WithNegativeValues_ShouldPersistInDatabase()
        {
            // Arrange
            var goal = new Goal { Calories = -200, Protein = -50, Carbs = -100, Fat = -30 };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == -200);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.AreEqual(-200, retrievedGoal.Calories);
            Assert.AreEqual(-50, retrievedGoal.Protein);
            Assert.AreEqual(-100, retrievedGoal.Carbs);
            Assert.AreEqual(-30, retrievedGoal.Fat);
        }

        [TestMethod]
        public void AddGoal_WithValidDataAndNullFat_ShouldPersistInDatabase()
        {
            // Arrange
            var goal = new Goal { Calories = 2000, Protein = 150, Carbs = 200, Fat = null };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == 2000);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.AreEqual(2000, retrievedGoal.Calories);
            Assert.AreEqual(150, retrievedGoal.Protein);
            Assert.AreEqual(200, retrievedGoal.Carbs);
            Assert.IsNull(retrievedGoal.Fat);
        }

        [TestMethod]
        public void AddGoal_WithMaximumIntValues_ShouldPersistInDatabase()
        {
            // Arrange
            var goal = new Goal
            {
                Calories = int.MaxValue,
                Protein = int.MaxValue,
                Carbs = int.MaxValue,
                Fat = int.MaxValue
            };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.Calories == int.MaxValue);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.AreEqual(int.MaxValue, retrievedGoal.Calories);
            Assert.AreEqual(int.MaxValue, retrievedGoal.Protein);
            Assert.AreEqual(int.MaxValue, retrievedGoal.Carbs);
            Assert.AreEqual(int.MaxValue, retrievedGoal.Fat);
        }
    }


}