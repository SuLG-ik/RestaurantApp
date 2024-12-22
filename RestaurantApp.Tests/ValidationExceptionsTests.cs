using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Domain;

namespace RestaurantApp.Tests;

[TestClass]
public class ValidationExceptionTests
{
    [TestMethod]
    public void ValidationException_ShouldSetPropertiesCorrectly()
    {
        var tag = "TestTag";
        var message = "TestMessage";

        var exception = new ValidationException(message);


        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationConvertException_ShouldSetPropertiesCorrectly()
    {
        var actual = "42";
        var expectedType = typeof(int);
        var tag = "TestTag";
        var message = "Invalid conversion";

        var exception = new ValidationConvertException<string>(actual, expectedType, message);

        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(expectedType, exception.ExpectedType);

        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationLengthException_ShouldSetPropertiesCorrectly()
    {
        var actual = "abc";
        var minLength = 5;
        var maxLength = 10;
        var tag = "TestTag";
        var message = "Length is out of range";

        var exception = new ValidationLengthException<string>(actual, minLength, maxLength, message);

        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(minLength, exception.MinLength);
        Assert.AreEqual(maxLength, exception.MaxLength);

        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationNotBlankException_ShouldSetPropertiesCorrectly()
    {
        var actual = "";
        var tag = "TestTag";
        var message = "Value cannot be blank";
        
        var exception = new ValidationNotBlankException(actual, message);
        
        Assert.AreEqual(actual, exception.Actual);

        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationNotCourseInException_ShouldSetPropertiesCorrectly()
    {
        var actual = 42;
        var minValue = 0;
        var maxValue = 100;
        var tag = "TestTag";
        var message = "Value is not within range";

        var exception = new ValidationNotCourseInException<int>(actual, minValue, maxValue, message);

        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(minValue, exception.MinValue);
        Assert.AreEqual(maxValue, exception.MaxValue);

        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationEqualsException_ShouldSetPropertiesCorrectly()
    {
        var actual = 42;
        var expected = 100;
        var tag = "TestTag";
        var message = "Values are not equal";


        var exception = new ValidationEqualsException<int>(actual, expected, message);

        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(expected, exception.Expected);

        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationNullException_ShouldSetPropertiesCorrectly()
    {
        var tag = "TestTag";
        var message = "Value cannot be null";

        var exception = new ValidationNullException(message);


        Assert.AreEqual(message, exception.Message);
    }
    
    [TestMethod]
    public void ValidationCheckException_ShouldSetPropertiesCorrectly()
    {
        var actual = "123";
        var tag = "TestTag";
        var message = "Value cannot be null";

        var exception = new ValidationCheckException<string>(actual, message);

        Assert.AreEqual(actual, exception.Actual);

        Assert.AreEqual(message, exception.Message);
    }
}