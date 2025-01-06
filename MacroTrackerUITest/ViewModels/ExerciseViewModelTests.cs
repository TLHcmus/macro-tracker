using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class ExerciseViewModelTests
{
    private Mock<IDaoSender> _mockDaoSender;
    private IServiceProvider _serviceProvider;
    private ExerciseViewModel _viewModel;

    [TestInitialize]
    public void Setup()
    {
        _mockDaoSender = new Mock<IDaoSender>();
        _mockDaoSender.Setup(sender => sender.GetExercises()).Returns([]);

        var serviceProvider = new Mock<IServiceProvider>();

        serviceProvider.Setup(
            service => service.GetService(typeof(IDaoSender))
        ).Returns(_mockDaoSender.Object);


        _serviceProvider = serviceProvider.Object;
        _viewModel = new ExerciseViewModel(_serviceProvider);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeExercises()
    {
        // Arrange
        var exercises = new List<Exercise>
            {
                new() { Name = "Running", CaloriesPerMinute = 10 },
                new() { Name = "Swimming", CaloriesPerMinute = 8 }
            };
        _mockDaoSender.Setup(sender => sender.GetExercises()).Returns(exercises);

        // Act
        var viewModel = new ExerciseViewModel(_serviceProvider);

        // Assert
        Assert.AreEqual(exercises.Count, viewModel.Exercises.Count);
    }
}
