using RestaurantApp.Formatter;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Formatter;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

[TestClass]
public class SavedModelFormatterTests
{
    private SavedModelFormatter _formatter;
    private Mock<IFormatter> _mockGeneralFormatter;

    private class TestClass;
    
    [TestInitialize]
    public void Setup()
    {
        _mockGeneralFormatter = new Mock<IFormatter>();
        _formatter = new SavedModelFormatter(_mockGeneralFormatter.Object);
    }

    [TestMethod]
    public void Format_ValidSavedModel_ShouldReturnFormattedString()
    {
        const int modelId = 1;
        const string modelData = "Test Data";
        var savedModel = new SavedModel<string>.Builder().SetId(modelId).SetData(modelData).Build();
        
        _mockGeneralFormatter.Setup(f => f.Format(modelData)).Returns("Formatted Test Data");

        Assert.IsTrue(_formatter.Supports(savedModel));
        var result = _formatter.Format(savedModel);

        Assert.AreEqual("ID: 1, Formatted Test Data", result);
        _mockGeneralFormatter.Verify(f => f.Format(modelData), Times.Once);
    }

    [TestMethod]
    public void Format_NullSavedModel_ShouldThrowArgumentNullException()
    {
        // Arrange
        SavedModel<object> nullModel = null;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => _formatter.Format(nullModel!));
    }

    [TestMethod]
    public void Supports_SavedModel_ShouldReturnTrue()
    {
        // Arrange
        var savedModel = new SavedModel<object>.Builder().SetId(1).SetData("Test Data").Build();

        // Act
        var result = _formatter.Supports(savedModel);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Supports_NonSavedModel_ShouldReturnFalse()
    {
        // Arrange
        var nonSavedModel = new object();

        // Act
        var result = _formatter.Supports(nonSavedModel);

        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void Supports_SavedModelOfAnyType_ShouldReturnTrue()
    {

        
        // Arrange
        var savedModelString = new SavedModel<string>.Builder().SetId(1).SetData("Test").Build();
        var savedModelInt = new SavedModel<TestClass>.Builder().SetId(2).SetData(new TestClass()).Build();

        // Act
        var resultString = _formatter.Supports(savedModelString);
        var resultInt = _formatter.Supports(savedModelInt);

        // Assert
        Assert.IsTrue(resultString);
        Assert.IsTrue(resultInt);
    }

}
