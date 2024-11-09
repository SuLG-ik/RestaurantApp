using System;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestaurantApp.Tests;

[TestClass]
public class ValidatorTests
{
    [TestMethod]
    public void RequireGreaterThan_ShouldReturnValue_WhenGreaterThanMin()
    {
        int result = Validator.RequireGreaterThan(10, 5, "TestValue");
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireGreaterThan_ShouldThrowException_WhenValueIsLessThanOrEqualToMin()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() =>
            Validator.RequireGreaterThan(5, 5, "TestValue"));
    }

    [TestMethod]
    public void RequireCourseIn_ShouldReturnValue_WhenValueIsInRange()
    {
        int result = Validator.RequireCourseIn(10, 5, 15, "TestValue");
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireCourseIn_ShouldThrowException_WhenValueIsOutsideRange()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() =>
            Validator.RequireCourseIn(20, 5, 15, "TestValue"));
    }

    [TestMethod]
    public void RequireEquals_ShouldReturnValue_WhenEqualToRequiredValue()
    {
        int result = Validator.RequireEquals(10, 10, "TestValue");
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireEquals_ShouldThrowException_WhenNotEqualToRequiredValue()
    {
        Assert.ThrowsException<ValidationEqualsException<int>>(() => Validator.RequireEquals(10, 20, "TestValue"));
    }

    [TestMethod]
    public void RequireNotEmpty_ShouldReturnValue_WhenNotEmpty()
    {
        string result = Validator.RequireNotEmpty("Test", "TestValue");
        Assert.AreEqual("Test", result);
    }

    [TestMethod]
    public void RequireNotEmpty_ShouldThrowException_WhenEmpty()
    {
        Assert.ThrowsException<ValidationLengthException<string>>(() => Validator.RequireNotEmpty("", "TestValue"));
    }

    [TestMethod]
    public void RequireNotNull_ShouldReturnValue_WhenNotNull()
    {
        string result = Validator.RequireNotNull("Test", "TestValue");
        Assert.AreEqual("Test", result);
    }

    [TestMethod]
    public void RequireNotNull_ShouldThrowException_WhenNull()
    {
        Assert.ThrowsException<ValidationNullException>(() => Validator.RequireNotNull<string>(null, "TestValue"));
    }

    [TestMethod]
    public void RequireEnum_ShouldReturnEnum_WhenValueIsValidEnum()
    {
        TestEnum result = Validator.RequireEnum<TestEnum>(1, "TestEnumValue");
        Assert.AreEqual(TestEnum.One, result);
    }

    [TestMethod]
    public void RequireEnum_ShouldThrowException_WhenValueIsNotDefinedEnum()
    {
        Assert.ThrowsException<ValidationConvertException<int>>(() => Validator.RequireEnum<TestEnum>(5, "TestEnumValue"));
    }

    [TestMethod]
    public void RequireInt_ShouldReturnInt_WhenValidString()
    {
        int result = Validator.RequireInt("123", "TestValue");
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void RequireInt_ShouldThrowException_WhenInvalidString()
    {
        Assert.ThrowsException<ValidationConvertException<string>>(() => Validator.RequireInt("abc", "TestValue"));
    }

    [TestMethod]
    public void RequireLong_ShouldReturnInt_WhenValidString()
    {
        long result = Validator.RequireLong("123", "TestValue");
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void RequireLong_ShouldThrowException_WhenInvalidString()
    {
        Assert.ThrowsException<ValidationConvertException<string>>(() => Validator.RequireLong("abc", "TestValue"));
    }

    [TestMethod]
    public void RequireBool_ShouldReturnTrue_WhenValidString()
    {
        bool result = Validator.RequireBool("true", "TestValue");
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void RequireBool_ShouldReturnFalse_WhenValidString()
    {
        bool result = Validator.RequireBool("false", "TestValue");
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void RequireBool_ShouldThrowException_WhenInvalidString()
    {
        Assert.ThrowsException<ValidationConvertException<string>>(() => Validator.RequireBool("abc", "TestValue"));
    }

    [TestMethod]
    public void RequireGreaterOrEqualsThan_ShouldReturnValue_WhenGreaterThanMin()
    {
        int result = Validator.RequireGreaterOrEqualsThan(10, 5, "TestValue");
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireGreaterOrEqualsThan_ShouldReturnMin_WhenValueEqualsMin()
    {
        int result = Validator.RequireGreaterOrEqualsThan(5, 5, "TestValue");
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void RequireGreaterOrEqualsThan_ShouldThrowException_WhenValueIsLessThanMin()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => Validator.RequireGreaterOrEqualsThan(4, 5, "TestValue"));
    }

    [TestMethod]
    public void RequireLessOrEqualsThan_ShouldReturnValue_WhenLessThanMax()
    {
        int result = Validator.RequireLessOrEqualsThan(10, 15, "TestValue");
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireLessOrEqualsThan_ShouldReturnMax_WhenValueEqualsMax()
    {
        int result = Validator.RequireLessOrEqualsThan(15, 15, "TestValue");
        Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void RequireLessOrEqualsThan_ShouldThrowException_WhenValueIsGreaterThanMax()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() =>
            Validator.RequireLessOrEqualsThan(20, 15, "TestValue"));
    }

    [TestMethod]
    public void RequireLessThan_ShouldReturnValue_WhenValueIsLessThanMax()
    {
        int result = Validator.RequireLessThan(10, 15, "TestValue");
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireLessThan_ShouldThrowException_WhenValuePlusOneIsGreaterOrEqualToMax()
    {
        Assert.ThrowsException<ValidationNotCourseInException<int>>(
            () => Validator.RequireLessThan(15, 15, "TestValue"));
    }

    [TestMethod]
    public void RequireNotBlank_ShouldReturnValue_WhenNotBlank()
    {
        string result = Validator.RequireNotBlank("  Test  ", "TestValue");
        Assert.AreEqual("  Test  ", result);
    }

    [TestMethod]
    public void RequireNotBlank_ShouldThrowException_WhenBlank()
    {
        Assert.ThrowsException<ValidationNotBlankException>(() => Validator.RequireNotBlank("   ", "TestValue"));
    }

    [TestMethod]
    public void RequireNotBlank_ShouldThrowException_WhenEmpty()
    {
        Assert.ThrowsException<ValidationNotBlankException>(() => Validator.RequireNotBlank("", "TestValue"));
    }

    [TestMethod]
    public void RequireNotNull_ShouldReturnValue_WhenNotNull_Class()
    {
        const string input = "Test";
        var result = Validator.RequireNotNull(input, "TestValue");
        Assert.AreEqual("Test", result);
    }

    [TestMethod]
    public void RequireNotNull_ShouldThrowException_WhenNull_Class()
    {
        Assert.ThrowsException<ValidationNullException>(() => Validator.RequireNotNull((string)null, "TestValue"));
    }

    [TestMethod]
    public void RequireNotNull_ShouldReturnValue_WhenNotNull_Struct()
    {
        const int input = 10;
        var result = Validator.RequireNotNull(input, "TestValue");
        Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void RequireNotNull_ShouldThrowException_WhenNull_Struct()
    {
        int? input = null;
        Assert.ThrowsException<ValidationNullException>(() => Validator.RequireNotNull(input, "TestValue"));
    }

    private class Counter
    {
        public int Count { get; set; }
    }


    [TestMethod]
    public void RunUntilValid_WithSuccessfulFunction_ReturnsResult()
    {
        // Arrange
        var expected = 42;
        Func<int> func = () => expected;

        // Act
        var result = Validator.RunUntilValid(func);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void RunUntilValid_WithValidationException_RetriesAndSucceeds()
    {
        // Arrange
        var counter = new Counter();
        Func<int> func = () =>
        {
            if (counter.Count < 2)
            {
                counter.Count++;
                throw new ValidationException("", "");
            }

            return 100;
        };
        bool onRetryCalled = false;
        Action onRetry = () => { onRetryCalled = true; };

        // Act
        var result = Validator.RunUntilValid(func, onRetry);

        // Assert
        Assert.AreEqual(2, counter.Count);
        Assert.AreEqual(100, result);
        Assert.IsTrue(onRetryCalled);
    }

    [TestMethod]
    public void RunUntilValid_WithValidationNullException_ThrowsConsoleIsNotAvailableException()
    {
        Func<int> func = () => throw new ValidationNullException("", "");

        Assert.ThrowsException<ConsoleNotAvailableException>(() => Validator.RunUntilValid(func));
    }

    [TestMethod]
    public void RunUntilValid_WithPersistentValidationException_ThrowsAfterMaxRetries()
    {
        var result = 200;
        var counter = new Counter();
        Func<int> func = () =>
        {
            if (counter.Count >= 5)
                return result;
            counter.Count++;
            throw new ValidationException("", "");
        };
        Assert.AreEqual(result, Validator.RunUntilValid(func));
    }

    [TestMethod]
    public void RunUntilValidVoid_WithSuccessfulFunction_CompletesSuccessfully()
    {
        // Arrange
        bool funcCalled = false;
        Action func = () => { funcCalled = true; };

        // Act
        Validator.RunUntilValid(func);

        // Assert
        Assert.IsTrue(funcCalled);
    }

    [TestMethod]
    public void RunUntilValidVoid_WithValidationException_RetriesAndSucceeds()
    {
        var counter = new Counter();
        Action func = () =>
        {
            if (counter.Count < 3)
            {
                counter.Count++;
                throw new ValidationException("", "");
            }
        };
        bool onRetryCalled = false;
        Action onRetry = () => { onRetryCalled = true; };

        Validator.RunUntilValid(func, onRetry);

        Assert.AreEqual(3, counter.Count);
        Assert.IsTrue(onRetryCalled);
    }

    [TestMethod]
    public void RunUntilValidVoid_WithValidationNullException_ThrowsConsoleIsNotAvailableException()
    {
        Action func = () => throw new ValidationNullException("TestValue", "");

        Assert.ThrowsException<ConsoleNotAvailableException>(() => Validator.RunUntilValid(func));
    }
}

// Example enum for enum-related tests
public enum TestEnum
{
    One = 1,
    Two = 2,
    Three = 3
}