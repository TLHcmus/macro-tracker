using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class GoalsViewModelTests
{
    private Mock<IDaoSender> _mockDaoSender;
    private Mock<IServiceProvider> _mockProvider;
    private GoalsViewModel _viewModel;

    [TestInitialize]
    public void Setup()
    {
        _mockDaoSender = new Mock<IDaoSender>();
        _mockProvider = new Mock<IServiceProvider>();
        _mockProvider.Setup(p => p.GetService(typeof(IDaoSender))).Returns(_mockDaoSender.Object);

        _viewModel = new GoalsViewModel(_mockProvider.Object);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeCurrentGoal()
    {
        // Arrange
        var expectedGoal = new Goal { Calories = 2000, Protein = 150, Carbs = 250, Fat = 70 };
        _mockDaoSender.Setup(sender => sender.GetGoal()).Returns(expectedGoal);

        // Act
        var viewModel = new GoalsViewModel(_mockProvider.Object);

        // Assert
        Assert.AreEqual(expectedGoal, viewModel.CurrentGoal);
    }

    [TestMethod]
    public void UpdateGoal_ShouldUpdateCurrentGoal()
    {
        // Arrange
        var newGoal = new Goal { Calories = 2200, Protein = 160, Carbs = 260, Fat = 80 };

        // Act
        _viewModel.UpdateGoal(newGoal);

        // Assert
        _mockDaoSender.Verify(sender => sender.UpdateGoal(newGoal), Times.Once);
        Assert.AreEqual(newGoal, _viewModel.CurrentGoal);
    }
}
