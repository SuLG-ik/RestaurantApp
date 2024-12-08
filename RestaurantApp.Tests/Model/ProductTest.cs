using JetBrains.Annotations;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[TestSubject(typeof(Product))]
[TestSubject(typeof(Product.Builder))]
public class ProductTests
{
    
    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateProduct()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit(Unit.Kg)
            .SetPrice(1.99m)
            .SetQuantity(100)
            .SetSupplierId(1);

        var product = builder.Build();

        Assert.AreEqual("Flour", product.Name);
        Assert.AreEqual(Unit.Kg, product.Unit);
        Assert.AreEqual(1.99m, product.Price);
        Assert.AreEqual(100, product.Quantity);
        Assert.AreEqual(1, product.SupplierId);
    }

    [TestMethod]
    public void SetName_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new Product.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetName(""));
    }

    [TestMethod]
    public void SetPrice_WithNegativeValue_ShouldThrowValidationNotCourseInException()
    {
        var builder = new Product.Builder();

        Assert.ThrowsException<ValidationNotCourseInException<decimal>>(() => builder.SetPrice(-5m));
    }

    [TestMethod]
    public void SetQuantity_WithNegativeValue_ShouldThrowValidationNotCourseInException()
    {
        var builder = new Product.Builder();

        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => builder.SetQuantity(-1));
    }

    [TestMethod]
    public void SetSupplier_WithValueLessThanZero_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder();

        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => builder.SetSupplierId(0));
    }

    [TestMethod]
    public void Build_WithoutName_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetUnit(Unit.Kg)
            .SetPrice(1.99m)
            .SetQuantity(100)
            .SetSupplierId(1);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutUnit_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetPrice(1.99m)
            .SetQuantity(100)
            .SetSupplierId(1);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutPrice_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit(Unit.Kg)
            .SetQuantity(100)
            .SetSupplierId(1);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutQuantity_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit(Unit.Kg)
            .SetPrice(1.99m)
            .SetSupplierId(1);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutSupplier_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit(Unit.Kg)
            .SetPrice(1.99m)
            .SetQuantity(100);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
}
