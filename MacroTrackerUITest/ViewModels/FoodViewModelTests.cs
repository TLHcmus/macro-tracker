using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class FoodViewModelTests
{
    private Mock<IDaoSender> _mockSender;
    private Mock<IServiceProvider> _mockProvider;
    private FoodViewModel _viewModel;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockSender = new Mock<IDaoSender>();
        _mockSender.Setup(sender => sender.GetFoods()).Returns([]);

        _mockProvider = new Mock<IServiceProvider>();
        _mockProvider.Setup(p => p.GetService(typeof(IDaoSender))).Returns(_mockSender.Object);

        _viewModel = new FoodViewModel(_mockProvider.Object);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeFoodsCollection()
    {
        // Arrange
        var foods = new List<Food>
            {
                new() { Name = "Apple" },
                new() { Name = "Banana" }
            };
        _mockSender.Setup(s => s.GetFoods()).Returns(foods);

        // Act
        var viewModel = new FoodViewModel(_mockProvider.Object);

        // Assert
        Assert.AreEqual(foods.Count, viewModel.Foods.Count);
        Assert.AreEqual(foods[0].Name, viewModel.Foods[0].Name);
        Assert.AreEqual(foods[1].Name, viewModel.Foods[1].Name);
    }

    [TestMethod]
    public void AddFood_ShouldAddFoodToCollection()
    {
        // Arrange
        var food = new Food { Name = "Orange" };

        // Act
        _viewModel.AddFood(food);

        // Assert
        _mockSender.Verify(s => s.AddFood(food), Times.Once);
        Assert.IsTrue(_viewModel.Foods.Contains(food));
    }
}
