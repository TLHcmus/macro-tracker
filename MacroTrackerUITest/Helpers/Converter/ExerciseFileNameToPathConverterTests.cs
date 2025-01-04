using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroTrackerUI.Helpers.Converter;
using System;
using MacroTrackerUI.Services.PathService;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace MacroTrackerUITest.Helpers.Converter;

[TestClass]
public class ExerciseFileNameToPathConverterTests
{
    private ExerciseFileNameToPathConverter _converter;

    [TestInitialize]
    public void Setup()
    {
        _converter = new ExerciseFileNameToPathConverter();
    }

    [UITestMethod]
    public void Convert_ValidFileName_ReturnsBitmapImage()
    {
        // Arrange
        string fileName = "baseball.png";
        string expectedUri = "ms-appx:///Assets/ExerciseIcons/baseball.png";

        // Act
        var result = _converter.Convert(fileName, null, null, null) as BitmapImage;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedUri, result.UriSource.ToString());
    }

    [UITestMethod]
    public void Convert_DefaultFileName_ReturnsDefaultBitmapImage()
    {
        // Arrange
        string fileName = "default.png";
        string expectedUri = "ms-appx:///Assets/ExerciseIcons/default.png";

        // Act
        var result = _converter.Convert(fileName, null, null, null) as BitmapImage;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedUri, result.UriSource.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void ConvertBack_ThrowsNotImplementedException()
    {
        // Act
        _converter.ConvertBack(null, null, null, null);
    }
}
