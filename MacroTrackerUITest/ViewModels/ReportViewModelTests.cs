using MacroTrackerUI.Models;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class ReportViewModelTests
{
    private Mock<IDaoSender> _mockSender;
    private Mock<IServiceProvider> _mockProvider;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockSender = new Mock<IDaoSender>();
        _mockProvider = new Mock<IServiceProvider>();
        _mockProvider.Setup(p => p.GetService(typeof(IDaoSender))).Returns(_mockSender.Object);

        _mockSender.Setup(p => p.GetLogs()).Returns([]);
        _mockSender.Setup(p => p.GetFoods()).Returns([]);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var logs = new List<Log>
            {
                new() {
                    LogDate = new DateOnly(2023, 10, 1),
                    TotalCalories = 2000,
                    LogFoodItems =
                    [
                        new() { FoodName = "Apple", NumberOfServings = 1 }
                    ]
                },
                new() {
                    LogDate = new DateOnly(2023, 10, 2),
                    TotalCalories = 2500,
                    LogFoodItems =
                    [
                        new() { FoodName = "Banana", NumberOfServings = 2 }
                    ]
                }
            };
        var foods = new List<Food>
            {
                new() { Name = "Apple", ProteinPer100g = 0.3, CarbsPer100g = 14, FatPer100g = 0.2 },
                new() { Name = "Banana", ProteinPer100g = 1.1, CarbsPer100g = 23, FatPer100g = 0.3 }
            };
        _mockSender.Setup(s => s.GetLogs()).Returns(logs);
        _mockSender.Setup(s => s.GetFoods()).Returns(foods);

        // Act
        var viewModel = new ReportViewModel(_mockProvider.Object);

        // Assert
        Assert.IsNotNull(viewModel.LogList);
        Assert.IsNotNull(viewModel.CaloriesSeries);
        Assert.IsNotNull(viewModel.ProteinSeries);
        Assert.IsNotNull(viewModel.CarbsSeries);
        Assert.IsNotNull(viewModel.FatSeries);
        Assert.IsNotNull(viewModel.XAxes);
        Assert.IsNotNull(viewModel.CaloriesYAxes);
        Assert.IsNotNull(viewModel.ProteinYAxes);
        Assert.IsNotNull(viewModel.CarbsYAxes);
        Assert.IsNotNull(viewModel.FatYAxes);
    }
}
