using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MacroTrackerUITest.Services.SenderService.DataAccessSender;

[TestClass]
public class DaoSenderTests
{
    private DaoSender _daoSender;
    private Mock<IDaoReceiver> _mockReceiver;

    [TestInitialize]
    public void Setup()
    {
        _mockReceiver = new Mock<IDaoReceiver>();
        var serviceProvider = new ServiceCollection()
            .AddSingleton(_mockReceiver.Object)
            .BuildServiceProvider();

        _daoSender = new DaoSender(serviceProvider);
    }

    [TestMethod]
    public void GetFoods_ShouldReturnListOfFoods()
    {
        // Arrange
        var foodsJson = JsonSerializer.Serialize(new List<Food> { new() { Name = "Apple" } });
        _mockReceiver.Setup(r => r.GetFoods()).Returns(foodsJson);

        // Act
        var result = _daoSender.GetFoods();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Apple", result[0].Name);
    }

    [TestMethod]
    public void AddFood_ShouldCallReceiverAddFood()
    {
        // Arrange
        var food = new Food { Name = "Banana" };

        // Act
        _daoSender.AddFood(food);

        // Assert
        _mockReceiver.Verify(r => r.AddFood(It.Is<string>(s => s.Contains("Banana"))), Times.Once);
    }

    [TestMethod]
    public void RemoveFood_ShouldCallReceiverRemoveFood()
    {
        // Arrange
        var foodName = "Orange";

        // Act
        _daoSender.RemoveFood(foodName);

        // Assert
        _mockReceiver.Verify(r => r.RemoveFood(It.Is<string>(s => s.Contains("Orange"))), Times.Once);
    }

    [TestMethod]
    public void GetExercises_ShouldReturnListOfExercises()
    {
        // Arrange
        var exercisesJson = JsonSerializer.Serialize(new List<Exercise> { new() { Name = "Running" } });
        _mockReceiver.Setup(r => r.GetExercises()).Returns(exercisesJson);

        // Act
        var result = _daoSender.GetExercises();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Running", result[0].Name);
    }

    [TestMethod]
    public void AddExercise_ShouldCallReceiverAddExercise()
    {
        // Arrange
        var exercise = new Exercise { Name = "Swimming" };

        // Act
        _daoSender.AddExercise(exercise);

        // Assert
        _mockReceiver.Verify(r => r.AddExercise(It.Is<string>(s => s.Contains("Swimming"))), Times.Once);
    }

    [TestMethod]
    public void RemoveExercise_ShouldCallReceiverRemoveExercise()
    {
        // Arrange
        var exerciseName = "Cycling";

        // Act
        _daoSender.RemoveExercise(exerciseName);

        // Assert
        _mockReceiver.Verify(r => r.RemoveExercise(It.Is<string>(s => s.Contains("Cycling"))), Times.Once);
    }

    [TestMethod]
    public void GetGoal_ShouldReturnGoal()
    {
        // Arrange
        var goalJson = JsonSerializer.Serialize(new Goal { Calories = 2000 });
        _mockReceiver.Setup(r => r.GetGoal()).Returns(goalJson);

        // Act
        var result = _daoSender.GetGoal();

        // Assert
        Assert.AreEqual(2000, result.Calories);
    }

    [TestMethod]
    public void UpdateGoal_ShouldCallReceiverUpdateGoal()
    {
        // Arrange
        var goal = new Goal { Calories = 2500 };

        // Act
        _daoSender.UpdateGoal(goal);

        // Assert
        _mockReceiver.Verify(r => r.UpdateGoal(It.Is<string>(s => s.Contains("2500"))), Times.Once);
    }

    [TestMethod]
    public void GetUsers_ShouldReturnListOfUsers()
    {
        // Arrange
        var usersJson = JsonSerializer.Serialize(new List<User> { new() { Username = "testuser" } });
        _mockReceiver.Setup(r => r.GetUsers()).Returns(usersJson);

        // Act
        var result = _daoSender.GetUsers();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("testuser", result[0].Username);
    }

    [TestMethod]
    public void DoesUserMatchPassword_ShouldReturnTrue()
    {
        // Arrange
        var username = "testuser";
        var password = "password";
        JsonSerializerOptions options = new() { IncludeFields = true };
        string signInJson = JsonSerializer.Serialize((username, password), options);
        _mockReceiver.Setup(r => r.DoesUserMatchPassword(signInJson)).Returns(JsonSerializer.Serialize(true));

        // Act
        var result = _daoSender.DoesUserMatchPassword(username, password);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DoesUsernameExist_ShouldReturnTrue()
    {
        // Arrange
        var username = "testuser";
        var usernameJson = JsonSerializer.Serialize(username);
        _mockReceiver.Setup(r => r.DoesUsernameExist(usernameJson)).Returns(JsonSerializer.Serialize(true));

        // Act
        var result = _daoSender.DoesUsernameExist(username);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void AddUser_ShouldCallReceiverAddUser()
    {
        // Arrange
        var user = ("testuser", "password");

        // Act
        _daoSender.AddUser(user);

        // Assert
        _mockReceiver.Verify(r => r.AddUser(It.Is<string>(s => s.Contains("testuser") && s.Contains("password"))), Times.Once);
    }

    [TestMethod]
    public void GetLogs_ShouldReturnListOfLogs()
    {
        // Arrange
        var logsJson = JsonSerializer.Serialize(new List<Log> { new() { LogId = 1 } });
        _mockReceiver.Setup(r => r.GetLogs()).Returns(logsJson);

        // Act
        var result = _daoSender.GetLogs();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].LogId);
    }

    [TestMethod]
    public void AddLog_ShouldCallReceiverAddLog()
    {
        // Arrange
        var log = new Log { LogId = 1 };

        // Act
        _daoSender.AddLog(log);

        // Assert
        _mockReceiver.Verify(r => r.AddLog(It.Is<string>(s => s.Contains('1'))), Times.Once);
    }

    [TestMethod]
    public void DeleteLog_ShouldCallReceiverDeleteLog()
    {
        // Arrange
        var logId = 1;

        // Act
        _daoSender.DeleteLog(logId);

        // Assert
        _mockReceiver.Verify(r => r.DeleteLog(It.Is<string>(s => s.Contains('1'))), Times.Once);
    }

    [TestMethod]
    public void DeleteLogFood_ShouldCallReceiverDeleteLogFood()
    {
        // Arrange
        var logDateID = 1;
        var logID = 2;

        // Act
        _daoSender.DeleteLogFood(logDateID, logID);

        // Assert
        _mockReceiver.Verify(r => r.DeleteLogFood(It.Is<string>(s => s.Contains('1') && s.Contains('2'))), Times.Once);
    }

    [TestMethod]
    public void DeleteLogExercise_ShouldCallReceiverDeleteLogExercise()
    {
        // Arrange
        var logDateID = 1;
        var logID = 2;

        // Act
        _daoSender.DeleteLogExercise(logDateID, logID);

        // Assert
        _mockReceiver.Verify(r => r.DeleteLogExercise(It.Is<string>(s => s.Contains('1') && s.Contains('2'))), Times.Once);
    }

    [TestMethod]
    public void GetLogWithPagination_ShouldReturnListOfLogs()
    {
        // Arrange
        var logsJson = JsonSerializer.Serialize(new List<Log> { new() { LogId = 1 } });
        string paginationJson = JsonSerializer.Serialize(
            (1, new DateOnly(2023, 1, 1)), new JsonSerializerOptions { IncludeFields = true }
        );
        _mockReceiver.Setup(r => r.GetLogWithPagination(paginationJson)).Returns(logsJson);

        // Act
        var result = _daoSender.GetLogWithPagination(1, new DateOnly(2023, 1, 1));

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].LogId);
    }

    [TestMethod]
    public void GetNLogWithPagination_ShouldReturnListOfLogs()
    {
        // Arrange
        var logsJson = JsonSerializer.Serialize(new List<Log> { new() { LogId = 1 } });
        string paginationJson = JsonSerializer.Serialize(
            (1, 1, new DateOnly(2023, 1, 1)), new JsonSerializerOptions { IncludeFields = true }
        );
        _mockReceiver.Setup(r => r.GetNLogWithPagination(paginationJson)).Returns(logsJson);

        // Act
        var result = _daoSender.GetNLogWithPagination(1, 1, new DateOnly(2023, 1, 1));

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].LogId);
    }

    [TestMethod]
    public void UpdateTotalCalories_ShouldCallReceiverUpdateTotalCalories()
    {
        // Arrange
        var logId = 1;
        var totalCalories = 500.0;

        // Act
        _daoSender.UpdateTotalCalories(logId, totalCalories);

        // Assert
        _mockReceiver.Verify(r => r.UpdateTotalCalories(It.Is<string>(s => s.Contains('1') && s.Contains("500"))), Times.Once);
    }
}
