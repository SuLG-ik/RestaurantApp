using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestaurantApp.Tests;

[TestClass]
public class StackNavigatorTests
{
    private StackNavigator<string> _navigator;

    [TestInitialize]
    public void Setup()
    {
        _navigator = new StackNavigator<string>("Home");
    }

    [TestMethod]
    public void InitialScreen_ShouldBeSetCorrectly()
    {
        var currentScreen = _navigator.CurrentScreen;


        Assert.AreEqual("Home", currentScreen);
    }

    [TestMethod]
    public void NavigateTo_ShouldAddScreenToBackstack()
    {
        _navigator.NavigateTo("Settings");
        var currentScreen = _navigator.CurrentScreen;


        Assert.AreEqual("Settings", currentScreen);
    }

    [TestMethod]
    public void ReplaceCurrent_ShouldReplaceCurrentScreen_WhenBackstackIsNotEmpty()
    {
        _navigator.NavigateTo("Settings");


        var result = _navigator.ReplaceCurrent("Profile");
        var currentScreen = _navigator.CurrentScreen;


        Assert.IsTrue(result);
        Assert.AreEqual("Profile", currentScreen);
    }

    [TestMethod]
    public void ReplaceCurrent_ShouldReturnFalse_WhenBackstackIsEmpty()
    {
        var emptyNavigator = new StackNavigator<string>();

        var result = emptyNavigator.ReplaceCurrent("Profile");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReplaceCurrentOrNavigate_ShouldReplace_WhenBackstackIsNotEmpty()
    {
        _navigator.NavigateTo("Settings");

        _navigator.ReplaceCurrentOrNavigate("Profile");
        var currentScreen = _navigator.CurrentScreen;

        Assert.AreEqual("Profile", currentScreen);
    }

    [TestMethod]
    public void ReplaceCurrentOrNavigate_ShouldNavigate_WhenBackstackIsEmpty()
    {
        var emptyNavigator = new StackNavigator<string>();
        
        emptyNavigator.ReplaceCurrentOrNavigate("Profile");
        var currentScreen = emptyNavigator.CurrentScreen;


        Assert.AreEqual("Profile", currentScreen);
    }

    [TestMethod]
    public void Back_ShouldRemoveCurrentScreen_WhenBackstackHasMoreThanOneScreen()
    {
        _navigator.NavigateTo("Settings");
        _navigator.NavigateTo("Profile");


        var result = _navigator.Back();
        var currentScreen = _navigator.CurrentScreen;


        Assert.IsTrue(result);
        Assert.AreEqual("Settings", currentScreen);
    }
    
    
    [TestMethod]
    public void Back_ShouldReturnFalse_WhenBackstackHasOnlyOneScreen()
    {
        var emptyNavigator = new StackNavigator<string>();
        var result = emptyNavigator.Back();
        var currentScreen = emptyNavigator.CurrentScreen;
        
        Assert.IsFalse(result);
        Assert.IsNull(currentScreen);
    }
    
    [TestMethod]
    public void Back_ShouldReturnTrue_WhenBackstackHasOnlyOneScreen()
    {
        var result = _navigator.Back();
        var currentScreen = _navigator.CurrentScreen;
        
        Assert.IsTrue(result);
        Assert.IsNull(currentScreen);
    }
    
    [TestMethod]
    public void Navigator_ShouldThrowArgumentNullException_WhenInitialScreenIsNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new StackNavigator<string>(null));
    }
}