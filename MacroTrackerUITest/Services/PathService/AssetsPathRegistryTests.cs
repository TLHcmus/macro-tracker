using MacroTrackerUI.Services.PathService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MacroTrackerUITest.Services.PathService;

[TestClass]
public class AssetsPathRegistryTests
{
    [TestMethod]
    public void RegisteredAssetsPath_ShouldContainExerciseIcon()
    {
        // Arrange
        var expectedKey = "ExerciseIcons";
        var expectedValue = "ms-appx:///Assets/ExerciseIcons";

        // Act
        var registeredAssetsPath = AssetsPathRegistry.RegisteredAssetsPath;

        // Assert
        Assert.IsTrue(registeredAssetsPath.ContainsKey(expectedKey), $"Key '{expectedKey}' is not found in the registered assets path.");
        Assert.AreEqual(expectedValue, registeredAssetsPath[expectedKey], $"Value for key '{expectedKey}' does not match the expected value.");
    }

    [TestMethod]
    public void RegisteredAssetsPath_ShouldNotBeNull()
    {
        // Act
        var registeredAssetsPath = AssetsPathRegistry.RegisteredAssetsPath;

        // Assert
        Assert.IsNotNull(registeredAssetsPath, "RegisteredAssetsPath should not be null.");
    }
}
