using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantApp.Formatter;

namespace RestaurantApp.Tests;

[TestClass]
public class SystemConsoleTests
{
    private SystemConsole _console;
    private Mock<IFormatter> _formatterMock = new();
    [TestInitialize]
    public void Setup()
    {
        _formatterMock.Setup(f => f.Supports(It.IsAny<object>())).Returns(false);
        _console = new SystemConsole(_formatterMock.Object);
    }

    [TestMethod]
    public void WriteLine_ShouldCallConsoleWriteLine()
    {
        // Arrange
        var value = "Test";
        using var consoleOutput = new ConsoleOutput();

        // Act
        _console.WriteLine(value);

        // Assert
        Assert.AreEqual(value + Environment.NewLine, consoleOutput.GetOutput());
    }

    [TestMethod]
    public void Write_ShouldCallConsoleWrite()
    {
        // Arrange
        var value = "Test";
        using var consoleOutput = new ConsoleOutput();

        // Act
        _console.Write(value);

        // Assert
        Assert.AreEqual(value, consoleOutput.GetOutput());
    }

    [TestMethod]
    public void ReadInt_ShouldReturnParsedInt()
    {
        // Arrange
        var input = "123";
        var tag = "TestTag";
        using var consoleInput = new ConsoleInput(input);

        // Act
        var result = _console.ReadInt(tag);

        // Assert
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void ReadIntUntilValid_ShouldReturnValidInt()
    {
        // Arrange
        var input = "invalid\n123";
        var tag = "TestTag";
        using var consoleInput = new ConsoleInput(input);

        // Act
        var result = _console.ReadIntUntilValid(tag, null);

        // Assert
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void ReadEnum_ShouldReturnParsedEnum()
    {
        // Arrange
        var input = "1";
        var tag = "TestTag";
        using var consoleInput = new ConsoleInput(input);

        // Act
        var result = _console.ReadEnum<TestEnum>(tag);

        // Assert
        Assert.AreEqual(TestEnum.Value1, result);
    }

    [TestMethod]
    public void ReadEnumUntilValid_ShouldReturnValidEnum()
    {
        // Arrange
        var input = "invalid\n1";
        using var consoleInput = new ConsoleInput(input);

        // Act
        var result = _console.ReadEnumUntilValid<TestEnum>();

        // Assert
        Assert.AreEqual(TestEnum.Value1, result);
    }

    [TestMethod]
    public void ReadString_ShouldReturnValidString()
    {
        // Arrange
        var input = "TestString";
        var tag = "TestTag";
        using var consoleInput = new ConsoleInput(input);

        // Act
        var result = _console.ReadString(tag);

        // Assert
        Assert.AreEqual("TestString", result);
    }

    [TestMethod]
    public void ReadStringUntilValid_ShouldReturnValidString()
    {
        // Arrange
        var input = "TestString";
        var tag = "TestTag";
        using var consoleInput = new ConsoleInput(input);

        // Act
        var result = _console.ReadStringUntilValid(tag, null);

        Assert.AreEqual("TestString", result);
    }

    private enum TestEnum
    {
        Value1 = 1,
    }
}

// Helper classes for capturing console input/output
public class ConsoleOutput : IDisposable
{
    private readonly StringWriter _stringWriter;
    private readonly TextWriter _originalOutput;

    public ConsoleOutput()
    {
        _stringWriter = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }

    public string GetOutput()
    {
        return _stringWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }
}

public class ConsoleInput : IDisposable
{
    private readonly StringReader _stringReader;
    private readonly TextReader _originalInput;

    public ConsoleInput(string input)
    {
        _stringReader = new StringReader(input);
        _originalInput = Console.In;
        Console.SetIn(_stringReader);
    }

    public void Dispose()
    {
        Console.SetIn(_originalInput);
        _stringReader.Dispose();
    }
}