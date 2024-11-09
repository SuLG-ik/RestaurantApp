using ConsoleApp1;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Model;

[TestClass]
[TestSubject(typeof(ProductRequestItem))]
[TestSubject(typeof(ProductRequestItem.Builder))]
public class ProductRequestItemTests
{
    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateProductRequestItem()
    {
        var builder = new ProductRequestItem.Builder()
            .SetProductName("Apple")
            .SetUnit("kg")
            .SetQuantity(5);

        var productRequestItem = builder.Build();

        Assert.AreEqual("Apple", productRequestItem.ProductName);
        Assert.AreEqual("kg", productRequestItem.Unit);
        Assert.AreEqual(5, productRequestItem.Quantity);
    }

    [TestMethod]
    public void SetProductName_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new ProductRequestItem.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetProductName(""));
    }

    [TestMethod]
    public void SetUnit_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new ProductRequestItem.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetUnit(""));
    }

    [TestMethod]
    public void SetQuantity_WithZeroOrLess_ShouldThrowValidationNotCourseInException()
    {
        var builder = new ProductRequestItem.Builder();

        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => builder.SetQuantity(0));
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => builder.SetQuantity(-5));
    }

    [TestMethod]
    public void Build_WithoutProductName_ShouldThrowValidationNotNullException()
    {
        var builder = new ProductRequestItem.Builder()
            .SetUnit("kg")
            .SetQuantity(5);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutUnit_ShouldThrowValidationNotNullException()
    {
        var builder = new ProductRequestItem.Builder()
            .SetProductName("Apple")
            .SetQuantity(5);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutQuantity_ShouldThrowValidationNotNullException()
    {
        var builder = new ProductRequestItem.Builder()
            .SetProductName("Apple")
            .SetUnit("kg");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
}
