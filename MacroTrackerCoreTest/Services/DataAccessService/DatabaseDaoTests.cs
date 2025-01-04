using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.EncryptionService;
using Moq;
using MacroTrackerCore.Services.ConfigurationService;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;

namespace MacroTrackerCoreTest.Services.DataAccessService;

[TestClass]
public class DatabaseDaoTests
{
    private DatabaseDao _databaseDao;

    [TestInitialize]
    public void Setup_Context_CreatesInMemoryDatabase()
    {
        _databaseDao = new DatabaseDao(true);
    }

    [TestMethod]
    public void GetFoods_ShouldReturnListOfFoods()
    {
        // Act
        var foods = _databaseDao.GetFoods();

        // Assert
        Assert.AreEqual(5, foods.Count);
    }

    [TestMethod]
    public void AddFood_ShouldAddFoodToDatabase()
    {
        // Arrange
        var food = new Food { Name = "Banana", CaloriesPer100g = 89, ProteinPer100g = 1.1, CarbsPer100g = 23, FatPer100g = 0.3 };

        // Act
        _databaseDao.AddFood(food);
        var foods = _databaseDao.GetFoods();

        // Assert
        Assert.IsTrue(foods.Contains(food));
    }

    [TestMethod]
    public void RemoveFood_ShouldRemoveFoodFromDatabase()
    {
        // Arrange
        var food = new Food { Name = "Orange", CaloriesPer100g = 47, ProteinPer100g = 0.9, CarbsPer100g = 12, FatPer100g = 0.1 };
        _databaseDao.AddFood(food);

        var addedFoods = _databaseDao.GetFoods();
        Assert.IsTrue(addedFoods.Contains(food));

        // Act
        _databaseDao.RemoveFood("Orange");
        var deletedFoods = _databaseDao.GetFoods();

        // Assert
        Assert.IsFalse(deletedFoods.Contains(food));
    }

    [TestMethod]
    public void GetExercises_ShouldReturnListOfExercises()
    {
        // Act
        var exercises = _databaseDao.GetExercises();

        // Assert
        Assert.AreEqual(5, exercises.Count);
    }

    [TestMethod]
    public void AddExercise_ShouldAddExerciseToDatabase()
    {
        // Arrange
        var exercise = new Exercise { Name = "Fencing", CaloriesPerMinute = 8 };

        // Act
        _databaseDao.AddExercise(exercise);
        var exercises = _databaseDao.GetExercises();

        // Assert
        Assert.IsTrue(exercises.Contains(exercise));
    }

    [TestMethod]
    public void RemoveExercise_ShouldRemoveExerciseFromDatabase()
    {
        // Arrange
        var exercise = new Exercise { Name = "Cycling", CaloriesPerMinute = 7 };
        _databaseDao.AddExercise(exercise);

        var addedExercises = _databaseDao.GetExercises();
        Assert.IsTrue(addedExercises.Contains(exercise));

        // Act
        _databaseDao.RemoveExercise("Cycling");
        var deletedExercises = _databaseDao.GetExercises();

        // Assert
        Assert.IsFalse(deletedExercises.Contains(exercise));
    }

    [TestMethod]
    public void GetGoal_ShouldReturnGoal()
    {
        // Arrange
        var goal = new Goal { Calories = 2000, Protein = 150, Carbs = 250, Fat = 70 };
        _databaseDao.UpdateGoal(goal);

        // Act
        var retrievedGoal = _databaseDao.GetGoal();

        // Assert
        Assert.IsNotNull(retrievedGoal);
        Assert.AreEqual(2000, retrievedGoal.Calories);
    }

    [TestMethod]
    public void UpdateGoal_ShouldUpdateExistingGoal()
    {
        // Arrange
        var goal = new Goal { Calories = 2000, Protein = 150, Carbs = 250, Fat = 70 };
        _databaseDao.UpdateGoal(goal);

        var updatedGoal = new Goal { Calories = 2200, Protein = 160, Carbs = 260, Fat = 80 };

        // Act
        _databaseDao.UpdateGoal(updatedGoal);
        var retrievedGoal = _databaseDao.GetGoal();

        // Assert
        Assert.IsNotNull(retrievedGoal);
        Assert.AreEqual(2200, retrievedGoal.Calories);
    }

    [TestMethod]
    public void GetUsers_ShouldReturnListOfUsers()
    {
        // Act
        var users = _databaseDao.GetUsers();

        // Assert
        Assert.AreEqual(1, users.Count);
    }

    [TestMethod]
    public void AddUser_ShouldAddUserToDatabase()
    {
        // Arrange
        var user = new User { Username = "newuser" };

        // Act
        _databaseDao.AddUser(user);
        var users = _databaseDao.GetUsers();

        // Assert
        Assert.IsTrue(users.Contains(user));
    }

    [TestMethod]
    public void DoesUserMatchPassword_ShouldReturnTrueIfPasswordMatches()
    {
        // Arrange
        var user = new User { Username = "testuser", EncryptedPassword = "encryptedpassword" };
        _databaseDao.AddUser(user);
        var passwordEncryption = new Mock<IPasswordEncryption>();
        passwordEncryption.Setup(pe => pe.EncryptPasswordToDatabase(It.IsAny<string>())).Returns("encryptedpassword");
        _databaseDao.PasswordEncryption = passwordEncryption.Object;

        // Act
        var result = _databaseDao.DoesUserMatchPassword("testuser", "encryptedpassword");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DoesUsernameExist_ShouldReturnTrueIfUsernameExists()
    {
        // Arrange
        var user = new User { Username = "existinguser" };
        _databaseDao.AddUser(user);

        // Act
        var result = _databaseDao.DoesUsernameExist("existinguser");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void GetLogs_ShouldReturnListOfLogs()
    {
        // Act
        var logs = _databaseDao.GetLogs();

        // Assert
        Assert.AreEqual(5, logs.Count);
    }

    [TestMethod]
    public void AddLog_ShouldAddLogToDatabase()
    {
        // Arrange
        var log = new Log { LogDate = DateOnly.FromDateTime(DateTime.Now), TotalCalories = 600 };

        // Act
        _databaseDao.AddLog(log);
        var logs = _databaseDao.GetLogs();

        // Assert
        Assert.IsTrue(logs.Contains(log));
    }

    [TestMethod]
    public void DeleteLog_ShouldRemoveLogFromDatabase()
    {
        // Arrange
        var log = new Log { LogDate = DateOnly.FromDateTime(DateTime.Now), TotalCalories = 700 };
        _databaseDao.AddLog(log);

        Assert.IsTrue(_databaseDao.GetLogs().Contains(log));

        // Act
        _databaseDao.DeleteLog(log.LogId);

        // Assert
        Assert.IsFalse(_databaseDao.GetLogs().Contains(log));
    }

    [TestMethod]
    public void GetLogWithPagination_ShouldReturnPaginatedLogs()
    {
        // Arrange
        for (int i = 0; i < 10; i++)
        {
            var log = new Log { LogDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-i)), TotalCalories = 100 * i };
            _databaseDao.AddLog(log);
        }
        int paginationNumber = Configuration.PAGINATION_NUMBER;

        // Act
        var logs = _databaseDao.GetLogWithPagination(
            0, 
            DateOnly.FromDateTime(DateTime.Now)
        );

        var logs2 = _databaseDao.GetLogWithPagination(
            0,
            DateOnly.FromDateTime(DateTime.Now.AddDays(-paginationNumber))
        );

        // Assert
        Assert.AreEqual(paginationNumber, logs.Count);
        Assert.AreEqual(paginationNumber, logs2.Count);
        Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now), logs[0].LogDate);
        Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(-paginationNumber)), logs2[0].LogDate);
    }
}
