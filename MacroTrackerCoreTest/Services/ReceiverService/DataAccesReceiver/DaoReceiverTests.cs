using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Text.Json;

namespace MacroTrackerCoreTest.Services.ReceiverService.DataAccesReceiver;

[TestClass]
public class DaoReceiverTests
{
    private Mock<IDao> _mockDao;
    private Mock<IPasswordEncryption> _mockPasswordEncryption;
    private DaoReceiver _daoReceiver;
    private JsonSerializerOptions _options; 

    [TestInitialize]
    public void Setup()
    {
        _mockDao = new Mock<IDao>();
        _mockPasswordEncryption = new Mock<IPasswordEncryption>();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(_mockDao.Object)
            .AddSingleton(_mockPasswordEncryption.Object)
            .BuildServiceProvider();

        _daoReceiver = new DaoReceiver(serviceProvider);

        _options = new() { IncludeFields = true };
    }

    [TestMethod]
    public void GetFoods_WhenCalled_ShouldReturnJsonString()
    {
        var foods = new List<Food> { new() { Name = "Apple" } };
        _mockDao.Setup(d => d.GetFoods()).Returns(foods);

        var result = _daoReceiver.GetFoods();

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<List<Food>>(result);

        Assert.IsNotNull(deserializedResult);
        Assert.AreEqual(foods.Count, deserializedResult.Count);
    }

    [TestMethod]
    public void AddFood_WhenCalled_ShouldCallDaoAddFood()
    {
        var food = new Food { Name = "Banana" };
        var foodJson = JsonSerializer.Serialize(food);

        _daoReceiver.AddFood(foodJson);

        _mockDao.Verify(d => d.AddFood(It.Is<Food>(f => f.Name == food.Name)), Times.Once);
    }

    [TestMethod]
    public void RemoveFood_WhenCalled_ShouldCallDaoRemoveFood()
    {
        var foodName = "Banana";
        var foodNameJson = JsonSerializer.Serialize(foodName);

        _daoReceiver.RemoveFood(foodNameJson);

        _mockDao.Verify(d => d.RemoveFood(foodName), Times.Once);
    }

    [TestMethod]
    public void GetExercises_WhenCalled_ShouldReturnJsonString()
    {
        var exercises = new List<Exercise> { new() { Name = "Running" } };
        _mockDao.Setup(d => d.GetExercises()).Returns(exercises);

        var result = _daoReceiver.GetExercises();

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<List<Exercise>>(result);
        Assert.IsNotNull(deserializedResult);
        Assert.AreEqual(exercises.Count, deserializedResult.Count);
    }

    [TestMethod]
    public void AddExercise_WhenCalled_ShouldCallDaoAddExercise()
    {
        var exercise = new Exercise { Name = "Swimming" };
        var exerciseJson = JsonSerializer.Serialize(exercise);

        _daoReceiver.AddExercise(exerciseJson);

        _mockDao.Verify(d => d.AddExercise(It.Is<Exercise>(e => e.Name == exercise.Name)), Times.Once);
    }

    [TestMethod]
    public void RemoveExercise_WhenCalled_ShouldCallDaoRemoveExercise()
    {
        var exerciseName = "Swimming";
        var exerciseNameJson = JsonSerializer.Serialize(exerciseName);

        _daoReceiver.RemoveExercise(exerciseNameJson);

        _mockDao.Verify(d => d.RemoveExercise(exerciseName), Times.Once);
    }

    [TestMethod]
    public void GetGoal_WhenCalled_ShouldReturnJsonString()
    {
        var goal = new Goal { GoalId = 0 };
        _mockDao.Setup(d => d.GetGoal()).Returns(goal);

        var result = _daoReceiver.GetGoal();

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<Goal>(result);
        Assert.IsNotNull(deserializedResult);
        Assert.AreEqual(goal.GoalId, deserializedResult.GoalId);
    }

    [TestMethod]
    public void UpdateGoal_WhenCalled_ShouldCallDaoUpdateGoal()
    {
        var goal = new Goal { GoalId = 0 };
        var goalJson = JsonSerializer.Serialize(goal);

        _daoReceiver.UpdateGoal(goalJson);

        _mockDao.Verify(d => d.UpdateGoal(It.Is<Goal>(g => g.GoalId == goal.GoalId)), Times.Once);
    }

    [TestMethod]
    public void GetUsers_WhenCalled_ShouldReturnJsonString()
    {
        var users = new List<User> { new() { Username = "testuser" } };
        _mockDao.Setup(d => d.GetUsers()).Returns(users);

        var result = _daoReceiver.GetUsers();

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<List<User>>(result);
        Assert.IsNotNull(deserializedResult);
        Assert.AreEqual(users.Count, deserializedResult.Count);
    }

    [TestMethod]
    public void DoesUserMatchPassword_WhenCalled_ShouldReturnJsonString()
    {
        var username = "testuser";
        var password = "password";
        var userJson = JsonSerializer.Serialize((username, password), _options);
        _mockDao.Setup(d => d.DoesUserMatchPassword(username, password)).Returns(true);

        var result = _daoReceiver.DoesUserMatchPassword(userJson);

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<bool>(result);
        Assert.IsTrue(deserializedResult);
    }

    [TestMethod]
    public void DoesUsernameExist_WhenCalled_ShouldReturnJsonString()
    {
        var username = "testuser";
        var usernameJson = JsonSerializer.Serialize(username);
        _mockDao.Setup(d => d.DoesUsernameExist(username)).Returns(true);

        var result = _daoReceiver.DoesUsernameExist(usernameJson);

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<bool>(result);
        Assert.IsTrue(deserializedResult);
    }

    [TestMethod]
    public void AddUser_WhenCalled_ShouldCallDaoAddUser()
    {
        var username = "newuser";
        var password = "newpassword";
        var userJson = JsonSerializer.Serialize((username, password), _options);
        _mockPasswordEncryption.Setup(
            pe => pe.EncryptPasswordToDatabase(password)
        ).Returns("encryptedPassword");

        _daoReceiver.AddUser(userJson);

        _mockDao.Verify(
            d => d.AddUser(
                It.Is<User>(u => u.Username == username && u.EncryptedPassword == "encryptedPassword")
            ), Times.Once
        );
    }

    [TestMethod]
    public void GetLogs_WhenCalled_ShouldReturnJsonString()
    {
        var logs = new List<Log> { new Log { LogId = 1 } };
        _mockDao.Setup(d => d.GetLogs()).Returns(logs);

        var result = _daoReceiver.GetLogs();

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<List<Log>>(result);
        Assert.IsNotNull(deserializedResult);
        Assert.AreEqual(logs.Count, deserializedResult.Count);
    }

    [TestMethod]
    public void AddLog_WhenCalled_ShouldCallDaoAddLog()
    {
        var log = new Log { LogId = 1 };
        var logJson = JsonSerializer.Serialize(log);

        _daoReceiver.AddLog(logJson);

        _mockDao.Verify(d => d.AddLog(It.Is<Log>(l => l.LogId == log.LogId)), Times.Once);
    }

    [TestMethod]
    public void DeleteLog_WhenCalled_ShouldCallDaoDeleteLog()
    {
        var logId = 1;
        var logIdJson = JsonSerializer.Serialize(logId);

        _daoReceiver.DeleteLog(logIdJson);

        _mockDao.Verify(d => d.DeleteLog(logId), Times.Once);
    }

    [TestMethod]
    public void DeleteLogFood_WhenCalled_ShouldCallDaoDeleteLogFood()
    {
        var idLogDate = 1;
        var idLog = 2;
        var idDeleteJson = JsonSerializer.Serialize((idLogDate, idLog), _options);

        _daoReceiver.DeleteLogFood(idDeleteJson);

        _mockDao.Verify(d => d.DeleteLogFood(idLogDate, idLog), Times.Once);
    }

    [TestMethod]
    public void DeleteLogExercise_WhenCalled_ShouldCallDaoDeleteLogExercise()
    {
        var idLogDate = 1;
        var idLog = 2;
        var idDeleteJson = JsonSerializer.Serialize((idLogDate, idLog), _options);

        _daoReceiver.DeleteLogExercise(idDeleteJson);

        _mockDao.Verify(d => d.DeleteLogExercise(idLogDate, idLog), Times.Once);
    }

    [TestMethod]
    public void GetLogWithPagination_WhenCalled_ShouldReturnJsonString()
    {
        var logs = new List<Log> { new() { LogId = 1 } };
        var pageOffsetJson = JsonSerializer.Serialize((10, DateOnly.FromDateTime(DateTime.Now)));
        _mockDao.Setup(d => d.GetLogWithPagination(It.IsAny<int>(), It.IsAny<DateOnly>())).Returns(logs);

        var result = _daoReceiver.GetLogWithPagination(pageOffsetJson);

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<List<Log>>(result);
        Assert.IsNotNull(deserializedResult);
        Assert.AreEqual(logs.Count, deserializedResult.Count);
    }

    [TestMethod]
    public void GetNLogWithPagination_WhenCalled_ShouldReturnJsonString()
    {
        var logs = new List<Log> { new() { LogId = 1 } };
        var pageOffsetJson = JsonSerializer.Serialize((5, 10, DateOnly.FromDateTime(DateTime.Now)));
        _mockDao.Setup(d => d.GetLogWithPagination(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateOnly>())).Returns(logs);

        var result = _daoReceiver.GetNLogWithPagination(pageOffsetJson);

        Assert.IsFalse(string.IsNullOrEmpty(result));
        var deserializedResult = JsonSerializer.Deserialize<List<Log>>(result);
        Assert.IsNotNull(deserializedResult);
        Assert.AreEqual(logs.Count, deserializedResult.Count);
    }

    [TestMethod]
    public void UpdateTotalCalories_WhenCalled_ShouldCallDaoUpdateTotalCalories()
    {
        var logId = 1;
        var totalCalories = 500.0;
        var logIdJson = JsonSerializer.Serialize((logId, totalCalories), _options);

        _daoReceiver.UpdateTotalCalories(logIdJson);

        _mockDao.Verify(d => d.UpdateTotalCalories(logId, totalCalories), Times.Once);
    }
}
