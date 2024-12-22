using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Domain;

namespace RestaurantApp.Tests;

[TestClass]
public class ValidatorTests
{
    [TestMethod]
    public void RequireGreaterThan_ShouldReturnValue_WhenGreaterThanMin()
    {
        int result = Validator.RequireGreaterThan(10, 5);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireGreaterThan_ShouldThrowException_WhenValueIsLessThanOrEqualToMin()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() =>
            Validator.RequireGreaterThan(5, 5));
    }

    [TestMethod]
    public void RequireCourseIn_ShouldReturnValue_WhenValueIsInRange()
    {
        int result = Validator.RequireCourseIn(10, 5, 15);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireCourseIn_ShouldThrowException_WhenValueIsOutsideRange()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() =>
            Validator.RequireCourseIn(20, 5, 15));
    }

    [TestMethod]
    public void RequireEquals_ShouldReturnValue_WhenEqualToRequiredValue()
    {
        int result = Validator.RequireEquals(10, 10);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireEquals_ShouldThrowException_WhenNotEqualToRequiredValue()
    {
        Assert.ThrowsException<ValidationEqualsException<int>>(() => Validator.RequireEquals(10, 20));
    }

    [TestMethod]
    public void RequireNotEmpty_ShouldReturnValue_WhenNotEmpty()
    {
        string result = Validator.RequireNotEmpty("Test");
        Assert.AreEqual("Test", result);
    }

    [TestMethod]
    public void RequireNotEmpty_ShouldThrowException_WhenEmpty()
    {
        Assert.ThrowsException<ValidationLengthException<string>>(() => Validator.RequireNotEmpty(""));
    }

    [TestMethod]
    public void RequireNotNull_ShouldReturnValue_WhenNotNull()
    {
        string result = Validator.RequireNotNull("Test");
        Assert.AreEqual("Test", result);
    }

    [TestMethod]
    public void RequireNotNull_ShouldThrowException_WhenNull()
    {
        Assert.ThrowsException<ValidationNullException>(() => Validator.RequireNotNull<string>(null));
    }

    [TestMethod]
    public void RequireEnum_ShouldReturnEnum_WhenValueIsValidEnum()
    {
        TestEnum result = Validator.RequireEnum<TestEnum>(1);
        Assert.AreEqual(TestEnum.One, result);
    }

    [TestMethod]
    public void RequireEnum_ShouldThrowException_WhenValueIsNotDefinedEnum()
    {
        Assert.ThrowsException<ValidationConvertException<int>>(() => Validator.RequireEnum<TestEnum>(5));
    }

    [TestMethod]
    public void RequireInt_ShouldReturnInt_WhenValidString()
    {
        int result = Validator.RequireInt("123");
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void RequireInt_ShouldThrowException_WhenInvalidString()
    {
        Assert.ThrowsException<ValidationConvertException<string>>(() => Validator.RequireInt("abc"));
    }

    [TestMethod]
    public void RequireDecimal_ShouldThrowException_WhenInvalidString()
    {
        Assert.ThrowsException<ValidationConvertException<string>>(() => Validator.RequireDecimal("abc"));
    }
    
    [TestMethod]
    public void RequireDecimal_ShouldReturnInt_WhenValidString()
    {
        var result = Validator.RequireDecimal("123");
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void RequireLong_ShouldReturnInt_WhenValidString()
    {
        long result = Validator.RequireLong("123");
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void RequireLong_ShouldThrowException_WhenInvalidString()
    {
        Assert.ThrowsException<ValidationConvertException<string>>(() => Validator.RequireLong("abc"));
    }

    [TestMethod]
    public void RequireBool_ShouldReturnTrue_WhenValidString()
    {
        bool result = Validator.RequireBool("true");
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void RequireBool_ShouldReturnFalse_WhenValidString()
    {
        bool result = Validator.RequireBool("false");
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void RequireBool_ShouldThrowException_WhenInvalidString()
    {
        Assert.ThrowsException<ValidationConvertException<string>>(() => Validator.RequireBool("abc"));
    }

    [TestMethod]
    public void RequireGreaterOrEqualsThan_ShouldReturnValue_WhenGreaterThanMin()
    {
        int result = Validator.RequireGreaterOrEqualsThan(10, 5);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireGreaterOrEqualsThan_ShouldReturnMin_WhenValueEqualsMin()
    {
        int result = Validator.RequireGreaterOrEqualsThan(5, 5);
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void RequireGreaterOrEqualsThan_ShouldThrowException_WhenValueIsLessThanMin()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => Validator.RequireGreaterOrEqualsThan(4, 5));
    }

    [TestMethod]
    public void RequireLessOrEqualsThan_ShouldReturnValue_WhenLessThanMax()
    {
        int result = Validator.RequireLessOrEqualsThan(10, 15);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireLessOrEqualsThan_ShouldReturnMax_WhenValueEqualsMax()
    {
        int result = Validator.RequireLessOrEqualsThan(15, 15);
        Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void RequireLessOrEqualsThan_ShouldThrowException_WhenValueIsGreaterThanMax()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() =>
            Validator.RequireLessOrEqualsThan(20, 15));
    }

    [TestMethod]
    public void RequireLessThan_ShouldReturnValue_WhenValueIsLessThanMax()
    {
        int result = Validator.RequireLessThan(10, 15);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireLessThan_ShouldThrowException_WhenValuePlusOneIsGreaterOrEqualToMax()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(
            () => Validator.RequireLessThan(15, 15));
    }

    [TestMethod]
    public void RequireNotBlank_ShouldReturnValue_WhenNotBlank()
    {
        string result = Validator.RequireNotBlank("  Test  ");
        Assert.AreEqual("  Test  ", result);
    }

    [TestMethod]
    public void RequireNotBlank_ShouldThrowException_WhenBlank()
    {
        Assert.ThrowsException<ValidationNotBlankException>(() => Validator.RequireNotBlank("   "));
    }

    [TestMethod]
    public void RequireNotBlank_ShouldThrowException_WhenEmpty()
    {
        Assert.ThrowsException<ValidationNotBlankException>(() => Validator.RequireNotBlank(""));
    }

    [TestMethod]
    public void RequireNotNull_ShouldReturnValue_WhenNotNull_Class()
    {
        const string input = "Test";
        var result = Validator.RequireNotNull(input);
        Assert.AreEqual("Test", result);
    }

    [TestMethod]
    public void RequireNotNull_ShouldThrowException_WhenNull_Class()
    {
        Assert.ThrowsException<ValidationNullException>(() => Validator.RequireNotNull((string)null));
    }

    [TestMethod]
    public void RequireNotNull_ShouldReturnValue_WhenNotNull_Struct()
    {
        const int input = 10;
        var result = Validator.RequireNotNull(input);
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireNotNull_ShouldThrowException_WhenNull_Struct()
    {
        int? input = null;
        Assert.ThrowsException<ValidationNullException>(() => Validator.RequireNotNull(input));
    }

    private class Counter
    {
        public int Count { get; set; }
    }

}

// Example enum for enum-related tests
public enum TestEnum
{
    One = 1,
    Two = 2,
    Three = 3
}