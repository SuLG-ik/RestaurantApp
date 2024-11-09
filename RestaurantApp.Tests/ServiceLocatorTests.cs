using System;
using System.Collections.Generic;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestaurantApp.Tests;

[TestClass]
public class ServiceLocatorTests
{
    [TestInitialize]
    [TestCleanup]
    public void Reset()
    {
        ServiceLocator.Reset();
    }

    [TestMethod]
    public void Register_Single_Should_Be_The_Same()
    {
        var service = new A();
        ServiceLocator.Register<IA>(service);
        Assert.AreSame(service, ServiceLocator.GetService<IA>());
    }

    [TestMethod]
    public void Register_Multiple_Should_Be_The_Same()
    {
        var serviceA = new A();
        var serviceB = new B();
        ServiceLocator.Register<IA>(serviceA);
        ServiceLocator.Register<IB>(serviceB);
        Assert.AreSame(serviceA, ServiceLocator.GetService<IA>());
        Assert.AreSame(serviceB, ServiceLocator.GetService<IB>());
    }

    [TestMethod]
    public void Register_Same_Multiple_Should_Throw_InvalidOperationException()
    {
        var serviceA1 = new A();
        var serviceA2 = new A();
        ServiceLocator.Register<IA>(serviceA1);
        Assert.ThrowsException<InvalidOperationException>(() => ServiceLocator.Register<IA>(serviceA2));
    }

    [TestMethod]
    public void Get_Unregistered_Should_Throw_KeyNotFoundException()
    {
        Assert.ThrowsException<KeyNotFoundException>(() => ServiceLocator.GetService<IA>());
    }

    [TestMethod]
    public void Reset_Should_Clear_Registered_Services()
    {
        ServiceLocator.Register<IA>(new A());
        ServiceLocator.Reset();
        Assert.ThrowsException<KeyNotFoundException>(() => ServiceLocator.GetService<IA>());
    }

    [TestMethod]
    public void Get_Upcasted_Key_Should_Throw_KeyNotFoundException()
    {
        ServiceLocator.Register<IA>(new A());
        Assert.ThrowsException<KeyNotFoundException>(() => ServiceLocator.GetService<A>());
    }

    private interface IA;

    private interface IB;

    private class A : IA;

    private class B : IB;
}