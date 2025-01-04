using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class LogItemViewModelTests
{
    private Mock<IDaoSender> _mockDaoSender;
    private LogItemViewModel _viewModel;

    [TestInitialize]
    public void Setup()
    {
        _mockDaoSender = new Mock<IDaoSender>();

        var serviceProvider = new Mock<IServiceProvider>();
        serviceProvider.Setup(x => x.GetService(typeof(IDaoSender))).Returns(_mockDaoSender.Object);

        _viewModel = new LogItemViewModel(serviceProvider.Object);
    }

    [TestMethod]
    public void UpdateTotalCalories_ShouldUpdateTotalCaloriesCorrectly()
    {
        // Arrange
        var log = new Log
        {
            LogId = 1,
            LogFoodItems =
                [
                    new LogFoodItem { TotalCalories = 200 },
                    new LogFoodItem { TotalCalories = 300 }
                ],
            LogExerciseItems =
                [
                    new LogExerciseItem { TotalCalories = 100 },
                    new LogExerciseItem { TotalCalories = 150 }
                ]
        };

        double expectedCalories = 750.0;

        // Act
        _viewModel.UpdateTotalCalories(log);

        // Assert
        _mockDaoSender.Verify(x => x.UpdateTotalCalories(log.LogId, expectedCalories), Times.Once);
        Assert.AreEqual(expectedCalories, log.TotalCalories);
    }

    [TestMethod]
    public void UpdateTotalCalories_ShouldHandleEmptyLogItems()
    {
        // Arrange
        var log = new Log
        {
            LogId = 1,
            LogFoodItems = [],
            LogExerciseItems = []
        };

        double expectedCalories = 0.0;

        // Act
        _viewModel.UpdateTotalCalories(log);

        // Assert
        _mockDaoSender.Verify(x => x.UpdateTotalCalories(log.LogId, expectedCalories), Times.Once);
        Assert.AreEqual(expectedCalories, log.TotalCalories);
    }
}
