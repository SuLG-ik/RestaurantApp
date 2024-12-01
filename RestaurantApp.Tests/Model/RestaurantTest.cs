using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Model;

[TestClass]
[TestSubject(typeof(Restaurant))]
[TestSubject(typeof(Restaurant.Builder))]
public class RestaurantTests
{
    private MenuItem _menuItem;

    [TestInitialize]
    public void Setup()
    {
        _menuItem = new MenuItem.Builder()
            .SetName("Pizza")
            .SetGroup(MenuItemGroup.SecondCourses)
            .SetPrice(15.99m)
            .Build();
    }

    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateRestaurant()
    {
        var builder = new Restaurant.Builder()
            .SetName("Test Restaurant")
            .SetAddress("123 Main St")
            .SetPhoneNumber("123456789")
            .SetDirectorFullname("John Doe")
            .AddMenuItem(_menuItem);

        var restaurant = builder.Build();

        Assert.AreEqual("Test Restaurant", restaurant.Name);
        Assert.AreEqual("123 Main St", restaurant.Address);
        Assert.AreEqual("123456789", restaurant.PhoneNumber);
        Assert.AreEqual("John Doe", restaurant.DirectorFullname);
        Assert.AreEqual(1, restaurant.Menu.Count);
        Assert.AreEqual(_menuItem, restaurant.Menu[0]);
    }

    [TestMethod]
    public void Build_WithoutName_ShouldThrowValidationNullException()
    {
        var builder = new Restaurant.Builder()
            .SetAddress("123 Main St")
            .SetPhoneNumber("123456789")
            .SetDirectorFullname("John Doe")
            .AddMenuItem(_menuItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void SetPhoneNumber_WithNonNumeric_ShouldThrowValidationConvertException()
    {
        var builder = new Restaurant.Builder();

        Assert.ThrowsException<ValidationCheckException<string>>(() => builder.SetPhoneNumber("InvalidPhoneNumber"));
    }

    [TestMethod]
    public void AddMenuItem_WithNull_ShouldThrowValidationNullException()
    {
        var builder = new Restaurant.Builder()
            .SetName("Test Restaurant")
            .SetAddress("123 Main St")
            .SetPhoneNumber("123456789")
            .SetDirectorFullname("John Doe");

        Assert.ThrowsException<ValidationNullException>(() => builder.AddMenuItem(null!));
    }

    [TestMethod]
    public void Build_WithoutAddress_ShouldThrowValidationNullException()
    {
        var builder = new Restaurant.Builder()
            .SetName("Test Restaurant")
            .SetPhoneNumber("123456789")
            .SetDirectorFullname("John Doe")
            .AddMenuItem(_menuItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutPhoneNumber_ShouldThrowValidationNullException()
    {
        var builder = new Restaurant.Builder()
            .SetName("Test Restaurant")
            .SetAddress("123 Main St")
            .SetDirectorFullname("John Doe")
            .AddMenuItem(_menuItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutDirectorFullname_ShouldThrowValidationNullException()
    {
        var builder = new Restaurant.Builder()
            .SetName("Test Restaurant")
            .SetAddress("123 Main St")
            .SetPhoneNumber("123456789")
            .AddMenuItem(_menuItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithEmptyMenu_ShouldCreateRestaurant()
    {
        var builder = new Restaurant.Builder()
            .SetName("Test Restaurant")
            .SetAddress("123 Main St")
            .SetPhoneNumber("123456789")
            .SetDirectorFullname("John Doe");

        var restaurant = builder.Build();

        Assert.AreEqual(0, restaurant.Menu.Count);
    }
}