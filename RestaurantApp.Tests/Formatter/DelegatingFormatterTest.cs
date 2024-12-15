using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Formatter;

namespace RestaurantApp.Tests.Formatter;

[TestClass]
[TestSubject(typeof(DelegatingFormatter))]
public class DelegatingFormatterTests
{
    private DelegatingFormatter _formatter;

    private readonly IFormatter _mockFormatter1 = new MockFormatter1();

    private class MockFormatter1 : IFormatter
    {
        public string Format(object value)
        {
            return "Formatter1";
        }

        public bool Supports(object value)
        {
            return true;
        }
    }

    private readonly IFormatter _mockFormatter2 = new MockFormatter2();
    
    private class MockFormatter2 : IFormatter
    {
        public string Format(object value)
        {
            return "Formatter2";
        }

        public bool Supports(object value)
        {
            return true;
        }
    }

    [TestInitialize]
    public void Setup()
    {
        _formatter = new DelegatingFormatter([_ => _mockFormatter1, _ => _mockFormatter2]);
    }

    [TestMethod]
    public void Format_ShouldUseFirstFormatterInOrder_IfMultipleSupport()
    {
        var result = _formatter.Format(new object());

        Assert.AreEqual("Formatter1", result);
    }
    

    [TestMethod]
    public void Format_ShouldThrowIfNoFormatterSupportsType()
    {
        var formatter = new DelegatingFormatter([]);
        Assert.ThrowsException<InvalidOperationException>(() => formatter.Format(new object()));
    }
}