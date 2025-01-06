using MacroTrackerUI.Helpers.Converter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MacroTrackerUITest.Helpers.Converter;

[TestClass]
public class DateTimeToStringConverterTests
{
    private DateTimeToStringConverter _converter;

    [TestInitialize]
    public void Setup()
    {
        _converter = new DateTimeToStringConverter();
    }

    [TestMethod]
    public void Convert_Today_ReturnsTodayString()
    {
        // Arrange
        var today = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = _converter.Convert(today, null, null, null);

        // Assert
        Assert.AreEqual("Today", result);
    }

    [TestMethod]
    public void Convert_Yesterday_ReturnsYesterdayString()
    {
        // Arrange
        var yesterday = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

        // Act
        var result = _converter.Convert(yesterday, null, null, null);

        // Assert
        Assert.AreEqual("Yesterday", result);
    }

    [TestMethod]
    public void Convert_OtherDate_ReturnsFormattedDateString()
    {
        // Arrange
        var date = new DateOnly(2023, 1, 1);

        // Act
        var result = _converter.Convert(date, null, null, null);

        // Assert
        Assert.AreEqual("January 01, 2023", result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Convert_InvalidType_ThrowsException()
    {
        // Arrange
        var invalidValue = "Not a DateOnly";

        // Act
        _converter.Convert(invalidValue, null, null, null);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void ConvertBack_AlwaysThrowsNotImplementedException()
    {
        // Act
        _converter.ConvertBack(null, null, null, null);

        // Assert is handled by ExpectedException
    }
}
