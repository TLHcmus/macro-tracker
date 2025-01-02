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
        public void AddGoal_WithValidDetails_ShouldPersistInDatabase()
        {
            // Arrange
            var goal = new Goal
            {
                Calories = 2500,
                Protein = 150,
                Carbs = 300,
                Fat = 70
            };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.AreEqual(2500, retrievedGoal.Calories);
            Assert.AreEqual(150, retrievedGoal.Protein);
            Assert.AreEqual(300, retrievedGoal.Carbs);
            Assert.AreEqual(70, retrievedGoal.Fat);
        }

        [TestMethod]
        public void GetGoal_ByExistingId_ShouldReturnCorrectGoal()
        {
            // Arrange
            var goal = new Goal
            {
                Calories = 2000,
                Protein = 120,
                Carbs = 250,
                Fat = 60
            };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.AreEqual(2000, retrievedGoal.Calories);
        }

        [TestMethod]
        public void UpdateGoal_WithValidValues_ShouldUpdateAllFields()
        {
            // Arrange
            var goal = new Goal
            {
                Calories = 2000,
                Protein = 120,
                Carbs = 250,
                Fat = 60
            };
            _context.Goals.Add(goal);
            _context.SaveChanges();

            // Act
            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);
            Assert.IsNotNull(retrievedGoal);

            retrievedGoal.Calories = 2500;
            retrievedGoal.Protein = 150;
            retrievedGoal.Carbs = 300;
            retrievedGoal.Fat = 70;
            _context.SaveChanges();

            var updatedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);

            // Assert
            Assert.IsNotNull(updatedGoal);
            Assert.AreEqual(2500, updatedGoal.Calories);
            Assert.AreEqual(150, updatedGoal.Protein);
            Assert.AreEqual(300, updatedGoal.Carbs);
            Assert.AreEqual(70, updatedGoal.Fat);
        }

        [TestMethod]
        public void DeleteGoal_ByExistingId_ShouldRemoveGoalFromDatabase()
        {
            // Arrange
            var goal = new Goal
            {
                Calories = 2000,
                Protein = 120,
                Carbs = 250,
                Fat = 60
            };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);
            Assert.IsNotNull(retrievedGoal);

            _context.Goals.Remove(retrievedGoal);
            _context.SaveChanges();

            var deletedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);

            // Assert
            Assert.IsNull(deletedGoal);
        }

        [TestMethod]
        public void GetAllGoals_WithMultipleGoals_ShouldReturnCorrectCount()
        {
            // Arrange & Act
            _context.Goals.Add(new Goal { Calories = 2000, Protein = 120, Carbs = 250, Fat = 60 });
            _context.Goals.Add(new Goal { Calories = 2500, Protein = 150, Carbs = 300, Fat = 70 });
            _context.Goals.Add(new Goal { Calories = 1800, Protein = 100, Carbs = 200, Fat = 50 });
            _context.SaveChanges();

            var goals = _context.Goals.ToList();

            // Assert - Adding 1 for the initial goal from database setup
            Assert.AreEqual(4, goals.Count);
        }

        [TestMethod]
        public void AddGoal_WithNullableFields_ShouldPersistInDatabase()
        {
            // Arrange
            var goal = new Goal
            {
                Calories = null,
                Protein = null,
                Carbs = 250,
                Fat = 60
            };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.IsNull(retrievedGoal.Calories);
            Assert.IsNull(retrievedGoal.Protein);
            Assert.AreEqual(250, retrievedGoal.Carbs);
            Assert.AreEqual(60, retrievedGoal.Fat);
        }

        [TestMethod]
        public void UpdateGoal_SetFieldsToNull_ShouldPersistNullValues()
        {
            // Arrange
            var goal = new Goal
            {
                Calories = 2000,
                Protein = 120,
                Carbs = 250,
                Fat = 60
            };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);
            Assert.IsNotNull(retrievedGoal);

            retrievedGoal.Calories = null;
            retrievedGoal.Protein = null;
            retrievedGoal.Carbs = null;
            retrievedGoal.Fat = null;
            _context.SaveChanges();

            var updatedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);

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
            var goal = new Goal
            {
                Calories = -2000,
                Protein = -120,
                Carbs = -250,
                Fat = -60
            };

            // Act
            _context.Goals.Add(goal);
            _context.SaveChanges();

            var retrievedGoal = _context.Goals.FirstOrDefault(g => g.GoalId == goal.GoalId);

            // Assert
            Assert.IsNotNull(retrievedGoal);
            Assert.AreEqual(-2000, retrievedGoal.Calories);
            Assert.AreEqual(-120, retrievedGoal.Protein);
            Assert.AreEqual(-250, retrievedGoal.Carbs);
            Assert.AreEqual(-60, retrievedGoal.Fat);
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

        [TestMethod]
        public void AddExercise_WithValidDetails_ShouldPersistInDatabase()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Fencing",
                CaloriesPerMinute = 10.5,
                IconFileName = "fencing.png"
            };

            // Act
            _context.Exercises.Add(exercise);
            _context.SaveChanges();

            var retrievedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Fencing");

            // Assert
            Assert.IsNotNull(retrievedExercise);
            Assert.AreEqual("Fencing", retrievedExercise.Name);
            Assert.AreEqual(10.5, retrievedExercise.CaloriesPerMinute);
            Assert.AreEqual("fencing.png", retrievedExercise.IconFileName);
        }

        [TestMethod]
        public void GetExercise_ByExistingName_ShouldReturnCorrectExercise()
        {
            // Arrange & Act
            var exercise = new Exercise
            {
                Name = "Cycling",
                CaloriesPerMinute = 8.0,
                IconFileName = "cycling.png"
            };

            _context.Exercises.Add(exercise);
            _context.SaveChanges();

            var retrievedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Cycling");

            // Assert
            Assert.IsNotNull(retrievedExercise);
            Assert.AreEqual(8.0, retrievedExercise.CaloriesPerMinute);
        }

        [TestMethod]
        public void UpdateExercise_WithValidValues_ShouldUpdateAllFields()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Walking",
                CaloriesPerMinute = 5.0,
                IconFileName = "walking.png"
            };

            _context.Exercises.Add(exercise);
            _context.SaveChanges();

            // Act
            var retrievedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Walking");
            Assert.IsNotNull(retrievedExercise);

            retrievedExercise.CaloriesPerMinute = 6.0;
            retrievedExercise.IconFileName = "walking_updated.png";
            _context.SaveChanges();

            var updatedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Walking");

            // Assert
            Assert.IsNotNull(updatedExercise);
            Assert.AreEqual(6.0, updatedExercise.CaloriesPerMinute);
            Assert.AreEqual("walking_updated.png", updatedExercise.IconFileName);
        }

        [TestMethod]
        public void DeleteExercise_WithNoLogItems_ShouldRemoveExerciseFromDatabase()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Fencing",
                CaloriesPerMinute = 12.0,
                IconFileName = "fencing.png"
            };

            // Act
            _context.Exercises.Add(exercise);
            _context.SaveChanges();

            var retrievedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Fencing");
            Assert.IsNotNull(retrievedExercise);

            _context.Exercises.Remove(retrievedExercise);
            _context.SaveChanges();

            var deletedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Fencing");

            // Assert
            Assert.IsNull(deletedExercise);
        }

        [TestMethod]
        public void AddExercise_WithNullableFields_ShouldPersistInDatabase()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Yoga",
                CaloriesPerMinute = null,
                IconFileName = null
            };

            // Act
            _context.Exercises.Add(exercise);
            _context.SaveChanges();

            var retrievedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Yoga");

            // Assert
            Assert.IsNotNull(retrievedExercise);
            Assert.IsNull(retrievedExercise.CaloriesPerMinute);
            Assert.IsNull(retrievedExercise.IconFileName);
        }

        [TestMethod]
        public void Exercise_WithLogItems_ShouldLoadRelatedItems()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Boxing",
                CaloriesPerMinute = 15.0,
                IconFileName = "boxing.png"
            };

            var log = new Log
            {
                LogDate = DateOnly.FromDateTime(DateTime.Now.Date),
                TotalCalories = 0
            };

            _context.Exercises.Add(exercise);
            _context.Logs.Add(log);
            _context.SaveChanges();

            var logExerciseItem = new LogExerciseItem
            {
                LogId = log.LogId,
                ExerciseName = exercise.Name,
                Duration = 30,
                TotalCalories = 450
            };

            // Act
            _context.LogExerciseItems.Add(logExerciseItem);
            _context.SaveChanges();

            var retrievedExercise = _context.Exercises
                .Include(e => e.LogExerciseItems)
                .FirstOrDefault(e => e.Name == "Boxing");

            // Assert
            Assert.IsNotNull(retrievedExercise);
            Assert.AreEqual(1, retrievedExercise.LogExerciseItems.Count);
            Assert.AreEqual(30, retrievedExercise.LogExerciseItems.First().Duration);
            Assert.AreEqual(450, retrievedExercise.LogExerciseItems.First().TotalCalories);
        }

        [TestMethod]
        public void DeleteExercise_WithLogItems_ShouldCascadeDelete()
        {
            // Arrange
            var exercise = new Exercise
            {
                Name = "Jumping",
                CaloriesPerMinute = 8.0,
                IconFileName = "jumping.png"
            };

            var log = new Log
            {
                LogDate = DateOnly.FromDateTime(DateTime.Now.Date),
                TotalCalories = 0
            };

            _context.Exercises.Add(exercise);
            _context.Logs.Add(log);
            _context.SaveChanges();

            var logExerciseItem = new LogExerciseItem
            {
                LogId = log.LogId,
                ExerciseName = exercise.Name,
                Duration = 20,
                TotalCalories = 160
            };

            _context.LogExerciseItems.Add(logExerciseItem);
            _context.SaveChanges();

            // Act
            var retrievedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Jumping");
            Assert.IsNotNull(retrievedExercise);

            _context.Exercises.Remove(retrievedExercise);
            _context.SaveChanges();

            var deletedExercise = _context.Exercises.FirstOrDefault(e => e.Name == "Jumping");
            var deletedLogItem = _context.LogExerciseItems.FirstOrDefault(l => l.ExerciseName == "Jumping");

            // Assert
            Assert.IsNull(deletedExercise);
            Assert.IsNull(deletedLogItem);
        }

        [TestMethod]
        public void AddExercise_WithDuplicateName_ShouldThrowException()
        {
            // Arrange
            var exercise1 = new Exercise
            {
                Name = "Pushups",
                CaloriesPerMinute = 8.0
            };

            // Act & Assert
            _context.Exercises.Add(exercise1);
            _context.SaveChanges();

            var exercise2 = new Exercise
            {
                Name = "Pushups",
                CaloriesPerMinute = 9.0
            };

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                _context.Exercises.Add(exercise2);
                _context.SaveChanges();
            });
        }

        [TestMethod]
        public void GetAllExercises_WithMultipleExercises_ShouldReturnCorrectCount()
        {
            // Arrange & Act
            _context.Exercises.Add(new Exercise { Name = "Exercise1", CaloriesPerMinute = 5.0 });
            _context.Exercises.Add(new Exercise { Name = "Exercise2", CaloriesPerMinute = 6.0 });
            _context.Exercises.Add(new Exercise { Name = "Exercise3", CaloriesPerMinute = 7.0 });
            _context.SaveChanges();

            var exercises = _context.Exercises.ToList();

            // Assert - Adding 5 for the initial exercises from database setup
            Assert.AreEqual(8, exercises.Count);
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

        [TestMethod]
        public void VerifySeededData_ShouldContainExpectedFoods()
        {
            // Act
            var foods = _context.Foods.ToList();

            // Assert
            Assert.AreEqual(5, foods.Count);

            var chickenBreast = foods.FirstOrDefault(f => f.Name == "Chicken breast");
            Assert.IsNotNull(chickenBreast);
            Assert.AreEqual(120, chickenBreast.CaloriesPer100g);
            Assert.AreEqual(22.5, chickenBreast.ProteinPer100g);
            Assert.AreEqual(0, chickenBreast.CarbsPer100g);
            Assert.AreEqual(2.6, chickenBreast.FatPer100g);
            Assert.AreEqual("chicken_breast.png", chickenBreast.IconFileName);
        }

        [TestMethod]
        public void AddFood_WithValidDetails_ShouldPersistInDatabase()
        {
            // Arrange
            var food = new Food
            {
                Name = "Banana",
                CaloriesPer100g = 89,
                ProteinPer100g = 1.1,
                CarbsPer100g = 22.8,
                FatPer100g = 0.3,
                IconFileName = "banana.png"
            };

            // Act
            _context.Foods.Add(food);
            _context.SaveChanges();

            var retrievedFood = _context.Foods.FirstOrDefault(f => f.Name == "Banana");

            // Assert
            Assert.IsNotNull(retrievedFood);
            Assert.AreEqual(89, retrievedFood.CaloriesPer100g);
            Assert.AreEqual(1.1, retrievedFood.ProteinPer100g);
            Assert.AreEqual(22.8, retrievedFood.CarbsPer100g);
            Assert.AreEqual(0.3, retrievedFood.FatPer100g);
        }

        [TestMethod]
        public void GetFood_WithLogItems_ShouldIncludeRelatedData()
        {
            // Arrange - Using seeded data
            var foodName = "Chicken breast";

            // Act
            var food = _context.Foods
                .Include(f => f.LogFoodItems)
                .FirstOrDefault(f => f.Name == foodName);

            // Assert
            Assert.IsNotNull(food);
            Assert.IsTrue(food.LogFoodItems.Count >= 2); // From seeded data

            var logItems = food.LogFoodItems.ToList();
            Assert.IsTrue(logItems.Any(l => l.NumberOfServings == 1.5 && l.TotalCalories == 180));
            Assert.IsTrue(logItems.Any(l => l.NumberOfServings == 2 && l.TotalCalories == 240));
        }

        [TestMethod]
        public void UpdateFood_WithValidValues_ShouldUpdateAllFields()
        {
            // Arrange - Using seeded data
            var food = _context.Foods.FirstOrDefault(f => f.Name == "White rice");
            Assert.IsNotNull(food);

            // Act
            food.CaloriesPer100g = 130;
            food.ProteinPer100g = 2.8;
            food.CarbsPer100g = 28.0;
            food.FatPer100g = 0.4;
            food.IconFileName = "white_rice_updated.png";
            _context.SaveChanges();

            var updatedFood = _context.Foods.FirstOrDefault(f => f.Name == "White rice");

            // Assert
            Assert.IsNotNull(updatedFood);
            Assert.AreEqual(130, updatedFood.CaloriesPer100g);
            Assert.AreEqual(2.8, updatedFood.ProteinPer100g);
            Assert.AreEqual(28.0, updatedFood.CarbsPer100g);
            Assert.AreEqual(0.4, updatedFood.FatPer100g);
            Assert.AreEqual("white_rice_updated.png", updatedFood.IconFileName);
        }

        [TestMethod]
        public void DeleteFood_WithLogItems_ShouldCascadeDelete()
        {
            // Arrange - Using seeded data
            var foodName = "Oat meal";
            var food = _context.Foods
                .Include(f => f.LogFoodItems)
                .FirstOrDefault(f => f.Name == foodName);

            Assert.IsNotNull(food);
            Assert.IsTrue(food.LogFoodItems.Any());

            // Act
            _context.Foods.Remove(food);
            _context.SaveChanges();

            var deletedFood = _context.Foods.FirstOrDefault(f => f.Name == foodName);
            var relatedLogItems = _context.LogFoodItems.Where(l => l.FoodName == foodName).ToList();

            // Assert
            Assert.IsNull(deletedFood);
            Assert.AreEqual(0, relatedLogItems.Count);
        }

        [TestMethod]
        public void AddFood_WithDuplicateName_ShouldThrowException()
        {
            // Arrange - Using seeded data
            var food = new Food
            {
                Name = "Beef", // Already exists in seeded data
                CaloriesPer100g = 150,
                ProteinPer100g = 20,
                CarbsPer100g = 0,
                FatPer100g = 7
            };

            // Act & Assert
            Assert.ThrowsException<DbUpdateException>(() =>
            {
                _context.Foods.Add(food);
                _context.SaveChanges();
            });
        }

        [TestMethod]
        public void VerifyFoodLogRelationships_ShouldMatchSeededData()
        {
            // Act
            var whiteRice = _context.Foods
                .Include(f => f.LogFoodItems)
                .FirstOrDefault(f => f.Name == "White rice");

            // Assert
            Assert.IsNotNull(whiteRice);
            var logItems = whiteRice.LogFoodItems.OrderBy(l => l.LogId).ToList();

            Assert.AreEqual(2, logItems.Count);

            // Verify first log entry
            Assert.AreEqual(2, logItems[0].NumberOfServings);
            Assert.AreEqual(258, logItems[0].TotalCalories);

            // Verify second log entry
            Assert.AreEqual(1.5, logItems[1].NumberOfServings);
            Assert.AreEqual(193.5, logItems[1].TotalCalories);
        }

        [TestMethod]
        public void AddFoodWithLogItem_ShouldCreateRelationshipCorrectly()
        {
            // Arrange
            var food = new Food
            {
                Name = "Apple",
                CaloriesPer100g = 52,
                ProteinPer100g = 0.3,
                CarbsPer100g = 14,
                FatPer100g = 0.2,
                IconFileName = "apple.png"
            };

            var log = new Log
            {
                LogDate = DateOnly.FromDateTime(DateTime.Now.Date),
                TotalCalories = 0
            };

            // Act
            _context.Foods.Add(food);
            _context.Logs.Add(log);
            _context.SaveChanges();

            var logFoodItem = new LogFoodItem
            {
                LogId = log.LogId,
                FoodName = food.Name,
                NumberOfServings = 2,
                TotalCalories = 104 // 52 * 2
            };

            _context.LogFoodItems.Add(logFoodItem);
            _context.SaveChanges();

            // Verify
            var retrievedFood = _context.Foods
                .Include(f => f.LogFoodItems)
                .FirstOrDefault(f => f.Name == "Apple");

            Assert.IsNotNull(retrievedFood);
            Assert.AreEqual(1, retrievedFood.LogFoodItems.Count);
            var relatedLogItem = retrievedFood.LogFoodItems.First();
            Assert.AreEqual(2, relatedLogItem.NumberOfServings);
            Assert.AreEqual(104, relatedLogItem.TotalCalories);
        }

        [TestMethod]
        public void AddFood_WithNullableFields_ShouldPersistInDatabase()
        {
            // Arrange
            var food = new Food
            {
                Name = "Mystery Food",
                CaloriesPer100g = null,
                ProteinPer100g = null,
                CarbsPer100g = null,
                FatPer100g = null,
                IconFileName = null
            };

            // Act
            _context.Foods.Add(food);
            _context.SaveChanges();

            var retrievedFood = _context.Foods.FirstOrDefault(f => f.Name == "Mystery Food");

            // Assert
            Assert.IsNotNull(retrievedFood);
            Assert.IsNull(retrievedFood.CaloriesPer100g);
            Assert.IsNull(retrievedFood.ProteinPer100g);
            Assert.IsNull(retrievedFood.CarbsPer100g);
            Assert.IsNull(retrievedFood.FatPer100g);
            Assert.IsNull(retrievedFood.IconFileName);
        }
    }
}