using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Model;

namespace RestaurantApp.Tests.Model;

[TestClass]
public class ProductRequestTests
{
    private ProductRequestItem _productRequestItem;

    [TestInitialize]
    public void Setup()
    {
        _productRequestItem = new ProductRequestItem.Builder()
            .SetProductId(1)
            .SetQuantity(1000)
            .Build();
    }

    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateProductRequest()
    {
        var requestDate = DateTime.Now;
        var builder = new ProductRequest.Builder()
            .SetRestaurantId(1)
            .SetRequestDate(requestDate)
            .AddProductRequestItem(_productRequestItem);

        var productRequest = builder.Build();

        Assert.AreEqual(1, productRequest.RestaurantId);
        Assert.AreEqual(requestDate, productRequest.RequestDate);
        Assert.AreEqual(1, productRequest.ProductRequestItems.Count);
        Assert.AreEqual(_productRequestItem, productRequest.ProductRequestItems[0]);
    }

    [TestMethod]
    public void Build_WithoutRestaurant_ShouldThrowValidationNullException()
    {
        var builder = new ProductRequest.Builder()
            .SetRequestDate(DateTime.Now)
            .AddProductRequestItem(_productRequestItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutProductRequestItems_ShouldThrowValidationNotEmptyException()
    {
        var builder = new ProductRequest.Builder()
            .SetRestaurantId(1)
            .SetRequestDate(DateTime.Now);

        Assert.ThrowsException<ValidationLengthException<List<ProductRequestItem>>>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutRequestDate_ShouldThrowValidationNullException()
    {
        var builder = new ProductRequest.Builder()
            .SetRestaurantId(1)
            .AddProductRequestItem(_productRequestItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void AddProductRequestItem_WithNull_ShouldThrowValidationNullException()
    {
        var builder = new ProductRequest.Builder()
            .SetRestaurantId(1)
            .SetRequestDate(DateTime.Now);

        Assert.ThrowsException<ValidationNullException>(() => builder.AddProductRequestItem(null!));
    }
}