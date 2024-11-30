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
            .SetProductId(1)
            .SetUnit(Unit.Gram)
            .SetPurchasePrice(10)
            .SetQuantity(5000);

        var productRequestItem = builder.Build();

        Assert.AreEqual(1, productRequestItem.ProductId);
        Assert.AreEqual(10, productRequestItem.PurchasePrice);
        Assert.AreEqual(Unit.Gram, productRequestItem.Unit);
        Assert.AreEqual(5000, productRequestItem.Quantity);
    }

    [TestMethod]
    public void SetProductId_WithValueLessThanZero_ShouldThrowValidationNotBlankException()
    {
        var builder = new ProductRequestItem.Builder();

        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => builder.SetProductId(0));
    }
    
    [TestMethod]
    public void SetPurchasePrice_WithValueLessThanZero_ShouldThrowValidationNotBlankException()
    {
        var builder = new ProductRequestItem.Builder();

        Assert.ThrowsException<ValidationNotCourseInException<decimal>>(() => builder.SetPurchasePrice(0));
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
            .SetUnit(Unit.Gram)
            .SetPurchasePrice(1000)
            .SetQuantity(5000);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutUnit_ShouldThrowValidationNotNullException()
    {
        var builder = new ProductRequestItem.Builder()
            .SetProductId(1)
            .SetPurchasePrice(1000)
            .SetQuantity(5);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutQuantity_ShouldThrowValidationNotNullException()
    {
        var builder = new ProductRequestItem.Builder()
            .SetProductId(1)
            .SetPurchasePrice(1000)
            .SetUnit(Unit.Gram);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
    
    [TestMethod]
    public void Build_WithoutPurchasePrice_ShouldThrowValidationNotNullException()
    {
        var builder = new ProductRequestItem.Builder()
            .SetProductId(1)
            .SetUnit(Unit.Gram)
            .SetQuantity(5);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
}
