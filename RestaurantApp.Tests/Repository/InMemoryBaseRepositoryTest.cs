using RestaurantApp.Model;
using RestaurantApp.Repository;

namespace RestaurantApp.Tests.Repository;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

[TestClass]
public class InMemoryBaseRepositoryTests
{
    private class TestRepository : InMemoryBaseRepository<string>
    {
        public TestRepository(List<SavedModel<string>> storage) : base(storage) { }
        public TestRepository() : base() { }
    }

    private TestRepository _repository;

    [TestInitialize]
    public void SetUp()
    {
        _repository = new TestRepository();
    }

    [TestMethod]
    public void Add_ShouldAddItemToRepository()
    {
        // Arrange
        var data = "Test Data";

        // Act
        var savedModel = _repository.Add(data);

        // Assert
        Assert.IsTrue(_repository.Exists(savedModel.Id));
        Assert.AreEqual(data, savedModel.Data);
    }

    [TestMethod]
    public void Update_ShouldUpdateExistingItemInRepository()
    {
        // Arrange
        var data = "Initial Data";
        var updatedData = "Updated Data";
        var savedModel = _repository.Add(data);

        // Act
        var updatedModel = _repository.Update(savedModel.Id, updatedData);
        var foundedModel = _repository.Find(savedModel.Id);
        
        // Assert
        Assert.IsTrue(_repository.Exists(savedModel.Id));
        Assert.AreEqual(updatedData, updatedModel.Data);
        Assert.IsNotNull(foundedModel);
        Assert.AreEqual(updatedData, foundedModel.Data);
    }

    [TestMethod]
    public void Update_NonExistingItem_ShouldThrowArgumentException()
    {
        // Arrange
        var nonExistentId = 999;
        var data = "New Data";

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _repository.Update(nonExistentId, data));
    }

    [TestMethod]
    public void Remove_ShouldRemoveItemFromRepository()
    {
        // Arrange
        var data = "Test Data";
        var savedModel = _repository.Add(data);

        // Act
        var removed = _repository.Remove(savedModel.Id);

        // Assert
        Assert.IsTrue(removed);
        Assert.IsFalse(_repository.Exists(savedModel.Id));
    }

    [TestMethod]
    public void Remove_NonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        var nonExistentId = 999;

        // Act
        var removed = _repository.Remove(nonExistentId);

        // Assert
        Assert.IsFalse(removed);
    }

    [TestMethod]
    public void Exists_ShouldReturnTrueForExistingItem()
    {
        // Arrange
        var data = "Test Data";
        var savedModel = _repository.Add(data);

        // Act
        var exists = _repository.Exists(savedModel.Id);

        // Assert
        Assert.IsTrue(exists);
    }

    [TestMethod]
    public void Exists_ShouldReturnFalseForNonExistingItem()
    {
        // Act
        var exists = _repository.Exists(999); // Non-existent ID

        // Assert
        Assert.IsFalse(exists);
    }

    [TestMethod]
    public void Find_ShouldReturnItemIfExists()
    {
        // Arrange
        var data = "Test Data";
        var savedModel = _repository.Add(data);

        // Act
        var foundModel = _repository.Find(savedModel.Id);

        // Assert
        Assert.IsNotNull(foundModel);
        Assert.AreEqual(savedModel.Id, foundModel.Id);
        Assert.AreEqual(savedModel.Data, foundModel.Data);
    }

    [TestMethod]
    public void Find_ShouldReturnNullIfItemDoesNotExist()
    {
        // Act
        var foundModel = _repository.Find(999); // Non-existent ID

        // Assert
        Assert.IsNull(foundModel);
    }

    [TestMethod]
    public void Constructor_WithStorage_ShouldInitializeWithGivenData()
    {
        // Arrange
        var initialData = new List<SavedModel<string>>
        {
            new SavedModel<string>.Builder().SetId(1).SetData("Data 1").Build(),
            new SavedModel<string>.Builder().SetId(2).SetData("Data 2").Build()
        };
        var repositoryWithStorage = new TestRepository(initialData);

        // Act & Assert
        Assert.IsTrue(repositoryWithStorage.Exists(1));
        Assert.IsTrue(repositoryWithStorage.Exists(2));
    }

    [TestMethod]
    public void Add_ShouldGenerateUniqueIds()
    {
        // Arrange
        var data1 = "Data 1";
        var data2 = "Data 2";

        // Act
        var savedModel1 = _repository.Add(data1);
        var savedModel2 = _repository.Add(data2);

        // Assert
        Assert.AreNotEqual(savedModel1.Id, savedModel2.Id);
    }
}
