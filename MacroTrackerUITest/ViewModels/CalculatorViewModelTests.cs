using MacroTrackerUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class CalculatorViewModelTests
{
    [TestMethod]
    public void CalculateTDEE_SedentaryMale_ReturnsCorrectTDEE()
    {
        // Arrange
        var viewModel = new CalculatorViewModel
        {
            Age = 25,
            Weight = 70,
            Height = 175,
            ActivityLevel = "Sedentary",
            Gender = "Male"
        };

        // Act
        double tdee = viewModel.CalculateTDEE();

        // Assert
        Assert.AreEqual(2008.5, tdee, 0.1);
    }

    [TestMethod]
    public void CalculateTDEE_LightlyActiveFemale_ReturnsCorrectTDEE()
    {
        // Arrange
        var viewModel = new CalculatorViewModel
        {
            Age = 30,
            Weight = 60,
            Height = 165,
            ActivityLevel = "Lightly Active",
            Gender = "Female"
        };

        // Act
        double tdee = viewModel.CalculateTDEE();

        // Assert
        Assert.AreEqual(1815.34375, tdee, 0.1);
    }

    [TestMethod]
    public void CalculateTDEE_ModeratelyActiveMale_ReturnsCorrectTDEE()
    {
        // Arrange
        var viewModel = new CalculatorViewModel
        {
            Age = 40,
            Weight = 80,
            Height = 180,
            ActivityLevel = "Moderately Active",
            Gender = "Male"
        };

        // Act
        double tdee = viewModel.CalculateTDEE();

        // Assert
        Assert.AreEqual(2681.5, tdee, 0.1);
    }

    [TestMethod]
    public void CalculateTDEE_VeryActiveFemale_ReturnsCorrectTDEE()
    {
        // Arrange
        var viewModel = new CalculatorViewModel
        {
            Age = 35,
            Weight = 55,
            Height = 160,
            ActivityLevel = "Very Active",
            Gender = "Female"
        };

        // Act
        double tdee = viewModel.CalculateTDEE();

        // Assert
        Assert.AreEqual(2094.15, tdee, 0.1);
    }

    [TestMethod]
    public void CalculateTDEE_SuperActiveMale_ReturnsCorrectTDEE()
    {
        // Arrange
        var viewModel = new CalculatorViewModel
        {
            Age = 28,
            Weight = 90,
            Height = 185,
            ActivityLevel = "Super Active",
            Gender = "Male"
        };

        // Act
        double tdee = viewModel.CalculateTDEE();

        // Assert
        Assert.AreEqual(3650.375, tdee, 0.1);
    }
}
