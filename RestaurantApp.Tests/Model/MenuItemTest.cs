using ConsoleApp1;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Model;

[TestClass]
[TestSubject(typeof(MenuItem))]
[TestSubject(typeof(MenuItem.Builder))]
public class MenuItemTests
{
    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateMenuItem()
    {
        // Arrange
        var builder = new MenuItem.Builder()
            .SetName("Burger")
            .SetGroup(ProductGroup.Appetisers)
            .SetPrice(10.99m);

        // Act
        var menuItem = builder.Build();

        // Assert
        Assert.AreEqual("Burger", menuItem.Name);
        Assert.AreEqual(ProductGroup.Appetisers, menuItem.Group);
        Assert.AreEqual(10.99m, menuItem.Price);
    }

    [TestMethod]
    public void SetName_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        // Arrange
        var builder = new MenuItem.Builder();

        // Act & Assert
        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetName(""));
    }

    [TestMethod]
    public void SetPrice_WithNegativeValue_ShouldThrowValidationNotCourseInException()
    {
        // Arrange
        var builder = new MenuItem.Builder();

        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<decimal>>(() => builder.SetPrice(-5m));
    }

    [TestMethod]
    public void Build_WithoutName_ShouldThrowValidationNotNullException()
    {
        // Arrange
        var builder = new MenuItem.Builder()
            .SetGroup(ProductGroup.Appetisers)
            .SetPrice(10.99m);

        // Act & Assert
        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutGroup_ShouldThrowValidationNotNullException()
    {
        // Arrange
        var builder = new MenuItem.Builder()
            .SetName("Burger")
            .SetPrice(10.99m);

        // Act & Assert
        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutPrice_ShouldThrowValidationNotNullException()
    {
        // Arrange
        var builder = new MenuItem.Builder()
            .SetName("Burger")
            .SetGroup(ProductGroup.Appetisers);

        // Act & Assert
        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
}