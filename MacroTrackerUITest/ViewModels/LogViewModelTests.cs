using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class LogViewModelTests
{
    private Mock<IDaoSender> _mockSender;
    private LogViewModel _viewModel;

    [TestInitialize]
    public void Setup()
    {
        _mockSender = new Mock<IDaoSender>();

        var serviceProvider = new Mock<IServiceProvider>();
        serviceProvider.Setup(x => x.GetService(typeof(IDaoSender))).Returns(_mockSender.Object);


        _viewModel = new LogViewModel(serviceProvider.Object);
    }

    [TestMethod]
    public void AddLog_ShouldAddLogToList()
    {
        // Arrange
        var log = new Log { LogId = 1, LogDate = DateOnly.FromDateTime(DateTime.Now) };

        // Act
        _viewModel.AddLog(log);

        // Assert
        Assert.IsTrue(_viewModel.LogList.Contains(log));
        _mockSender.Verify(s => s.AddLog(log), Times.Once);
    }

    [TestMethod]
    public void DeleteLog_ShouldRemoveLogFromList()
    {
        // Arrange
        var log = new Log { LogId = 1, LogDate = DateOnly.FromDateTime(DateTime.Now) };
        _viewModel.LogList.Add(log);

        // Act
        _viewModel.DeleteLog(log.LogId);

        // Assert
        Assert.IsFalse(_viewModel.LogList.Contains(log));
        _mockSender.Verify(s => s.DeleteLog(log.LogId), Times.Once);
    }

    [TestMethod]
    public void GetNextLogsPage_ShouldRetrieveLogs()
    {
        // Arrange
        var logs = new List<Log>
    {
        new() { LogId = 1, LogDate = DateOnly.FromDateTime(DateTime.Now) },
        new() { LogId = 2, LogDate = DateOnly.FromDateTime(DateTime.Now) }
    };
        _mockSender.Setup(s => s.GetLogWithPagination(It.IsAny<int>(), It.IsAny<DateOnly>())).Returns(logs);

        // Act
        _viewModel.GetNextLogsPage();

        // Assert
        Assert.AreEqual(logs.Count, _viewModel.LogList.Count);
        foreach (var log in logs)
        {
            Assert.IsTrue(_viewModel.LogList.Contains(log));
        }
    }

    [TestMethod]
    public void GetNextLogsItem_ShouldRetrieveLogs()
    {
        // Arrange
        var logs = new List<Log>
    {
        new() { LogId = 1, LogDate = DateOnly.FromDateTime(DateTime.Now) },
        new() { LogId = 2, LogDate = DateOnly.FromDateTime(DateTime.Now) }
    };
        _mockSender.Setup(s => s.GetNLogWithPagination(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateOnly>())).Returns(logs);

        // Act
        _viewModel.GetNextLogsItem(2);

        // Assert
        Assert.AreEqual(logs.Count, _viewModel.LogList.Count);
        foreach (var log in logs)
        {
            Assert.IsTrue(_viewModel.LogList.Contains(log));
        }
    }

    [TestMethod]
    public void DoesContainDate_ShouldReturnTrueIfDateExists()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now);
        var log = new Log { LogId = 1, LogDate = date };
        _viewModel.LogList.Add(log);

        // Act
        var result = _viewModel.DoesContainDate(date);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DoesContainDate_ShouldReturnFalseIfDateDoesNotExist()
    {
        // Arrange
        var date = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = _viewModel.DoesContainDate(date);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void DeleteLogFood_ShouldRemoveLogFoodItem()
    {
        // Arrange
        var log = new Log { LogId = 1, LogDate = DateOnly.FromDateTime(DateTime.Now) };
        var logFood = new LogFoodItem { LogFoodId = 1 };
        log.LogFoodItems = [logFood];
        _viewModel.LogList.Add(log);

        // Act
        _viewModel.DeleteLogFood(log.LogId, logFood.LogFoodId);

        // Assert
        Assert.IsFalse(log.LogFoodItems.Contains(logFood));
        _mockSender.Verify(s => s.DeleteLogFood(log.LogId, logFood.LogFoodId), Times.Once);
    }

    [TestMethod]
    public void DeleteLogExercise_ShouldRemoveLogExerciseItem()
    {
        // Arrange
        var log = new Log { LogId = 1, LogDate = DateOnly.FromDateTime(DateTime.Now) };
        var logExercise = new LogExerciseItem { LogExerciseId = 1 };
        log.LogExerciseItems = [logExercise];
        _viewModel.LogList.Add(log);

        // Act
        _viewModel.DeleteLogExercise(log.LogId, logExercise.LogExerciseId);

        // Assert
        Assert.IsFalse(log.LogExerciseItems.Contains(logExercise));
        _mockSender.Verify(s => s.DeleteLogExercise(log.LogId, logExercise.LogExerciseId), Times.Once);
    }
}