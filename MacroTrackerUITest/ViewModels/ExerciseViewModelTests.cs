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

    [TestMethod]
    public void AddExercise_ShouldAddExerciseToCollection()
    {
        // Arrange
        var exercise = new Exercise { Name = "Cycling", CaloriesPerMinute = 7 };

        // Act
        _viewModel.AddExercise(exercise);

        // Assert
        _mockDaoSender.Verify(sender => sender.AddExercise(exercise), Times.Once);
        Assert.IsTrue(_viewModel.Exercises.Contains(exercise));
    }

    [TestMethod]
    public void RemoveExercise_ShouldRemoveExerciseFromCollection()
    {
        // Arrange
        var exercise = new Exercise { Name = "Yoga", CaloriesPerMinute = 3 };
        _viewModel.Exercises.Add(exercise);

        // Act
        _viewModel.RemoveExercise(exercise.Name);

        // Assert
        _mockDaoSender.Verify(sender => sender.RemoveExercise(exercise.Name), Times.Once);
        Assert.IsFalse(_viewModel.Exercises.Contains(exercise));
    }

    [TestMethod]
    public void RemoveExercise_ShouldNotRemoveNonExistentExercise()
    {
        // Arrange
        var exercise = new Exercise { Name = "Pilates", CaloriesPerMinute = 4 };

        // Act
        _viewModel.RemoveExercise(exercise.Name);

        // Assert
        _mockDaoSender.Verify(sender => sender.RemoveExercise(exercise.Name), Times.Once);
        Assert.IsFalse(_viewModel.Exercises.Contains(exercise));
    }
}
