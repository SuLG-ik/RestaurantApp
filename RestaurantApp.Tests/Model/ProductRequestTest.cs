using System;
using System.Collections.Generic;
using ConsoleApp1;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Model;

[TestClass]
[TestSubject(typeof(ProductRequest))]
public class ProductRequestTests
{
    private Restaurant _restaurant;
    private ProductRequestItem _productRequestItem;

    [TestInitialize]
    public void Setup()
    {
        _restaurant = new Restaurant.Builder()
            .SetName("Test Restaurant")
            .SetAddress("123 Main St")
            .SetPhoneNumber("123456789")
            .SetDirectorFullname("John Doe")
            .Build();

        _productRequestItem = new ProductRequestItem.Builder()
            .SetProductName("Apple")
            .SetUnit("kg")
            .SetQuantity(10)
            .Build();
    }

    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateProductRequest()
    {
        var requestDate = DateTime.Now;
        var builder = new ProductRequest.Builder()
            .SetRestaurant(_restaurant)
            .SetRequestDate(requestDate)
            .AddProductRequestItem(_productRequestItem);

        var productRequest = builder.Build();

        Assert.AreEqual(_restaurant, productRequest.Restaurant);
        Assert.AreEqual(requestDate, productRequest.RequestDate);
        Assert.AreEqual(1, productRequest.ProductRequestItems.Count);
        Assert.AreEqual(_productRequestItem, productRequest.ProductRequestItems[0]);
    }

    [TestMethod]
    public void SetRestaurant_WithNull_ShouldThrowValidationNullException()
    {
        var builder = new ProductRequest.Builder()
            .SetRequestDate(DateTime.Now)
            .AddProductRequestItem(_productRequestItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.SetRestaurant(null!));
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
            .SetRestaurant(_restaurant)
            .SetRequestDate(DateTime.Now);

        Assert.ThrowsException<ValidationLengthException<List<ProductRequestItem>>>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutRequestDate_ShouldThrowValidationNullException()
    {
        var builder = new ProductRequest.Builder()
            .SetRestaurant(_restaurant)
            .AddProductRequestItem(_productRequestItem);

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void AddProductRequestItem_WithNull_ShouldThrowValidationNullException()
    {
        var builder = new ProductRequest.Builder()
            .SetRestaurant(_restaurant)
            .SetRequestDate(DateTime.Now);

        Assert.ThrowsException<ValidationNullException>(() => builder.AddProductRequestItem(null!));
    }
}