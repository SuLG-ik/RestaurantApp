using ConsoleApp1;
using JetBrains.Annotations;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[TestSubject(typeof(Product))]
[TestSubject(typeof(Product.Builder))]
public class ProductTests
{
    private static Supplier _supplier;

    [TestInitialize]
    public void Setup()
    {
        _supplier = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetAddress("Address")
            .SetDirector("Director")
            .SetPhoneNumber("123456789")
            .SetBank("Bank")
            .SetAccountNumber("123456789")
            .SetInn("123456789")
            .Build();

    }
    
    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateProduct()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit("kg")
            .SetPrice(1.99m)
            .SetQuantity(100)
            .SetSupplier(_supplier);

        var product = builder.Build();

        Assert.AreEqual("Flour", product.Name);
        Assert.AreEqual("kg", product.Unit);
        Assert.AreEqual(1.99m, product.Price);
        Assert.AreEqual(100, product.Quantity);
        Assert.AreEqual(_supplier, product.Supplier);
    }

    [TestMethod]
    public void SetName_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new Product.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetName(""));
    }

    [TestMethod]
    public void SetUnit_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new Product.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetUnit(""));
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
    public void SetSupplier_WithNull_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder();

        Assert.ThrowsException<ValidationNullException>(() => builder.SetSupplier(null!));
    }

    [TestMethod]
    public void Build_WithoutName_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetUnit("kg")
            .SetPrice(1.99m)
            .SetQuantity(100)
            .SetSupplier(_supplier);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutUnit_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetPrice(1.99m)
            .SetQuantity(100)
            .SetSupplier(_supplier);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutPrice_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit("kg")
            .SetQuantity(100)
            .SetSupplier(_supplier);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutQuantity_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit("kg")
            .SetPrice(1.99m)
            .SetSupplier(_supplier);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutSupplier_ShouldThrowValidationNotNullException()
    {
        var builder = new Product.Builder()
            .SetName("Flour")
            .SetUnit("kg")
            .SetPrice(1.99m)
            .SetQuantity(100);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
}
