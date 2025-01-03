using MacroTrackerUI.Helpers.Converter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MacroTrackerUITest.Helpers.Converter;

[TestClass]
public class FloatRoundingConverterTests
{
    private FloatRoundingConverter _converter;

    [TestInitialize]
    public void Setup()
    {
        _converter = new FloatRoundingConverter();
    }

    [TestMethod]
    public void Convert_NullValue_ReturnsZeroString()
    {
        var result = _converter.Convert(null, null, null, null);
        Assert.AreEqual("0", result);
    }

    [TestMethod]
    public void Convert_DoubleValue_ReturnsRoundedString()
    {
        var result = _converter.Convert(123.456, null, null, null);
        Assert.AreEqual("123.5", result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Convert_NonDoubleValue_ThrowsException()
    {
        _converter.Convert("not a double", null, null, null);
    }

    [TestMethod]
    public void ConvertBack_ThrowsNotImplementedException()
    {
        Assert.ThrowsException<NotImplementedException>(() => _converter.ConvertBack(null, null, null, null));
    }
}
