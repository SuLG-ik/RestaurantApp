using System.Linq;
using RestaurantApp.Model;
using RestaurantApp.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RestaurantApp.Tests
{
    [TestClass]
    public class InMemoryBaseRepositoryTests
    {
        private class TestModel
        {
            public string Name { get; set; } = string.Empty;
        }

        private InMemoryBaseRepository<TestModel> CreateRepository(IEnumerable<SavedModel<TestModel>>? initialStorage = null)
        {
            return new TestRepository<TestModel>(initialStorage ?? new List<SavedModel<TestModel>>());
        }

        [TestMethod]
        public void Constructor_WithEmptyStorage_ShouldInitializeCorrectly()
        {
            var repository = CreateRepository();

            Assert.AreEqual(0, repository.FindAll().Count);
        }

        [TestMethod]
        public void Constructor_WithExistingStorage_ShouldInitializeCorrectly()
        {
            var initialStorage = new List<SavedModel<TestModel>>
            {
                new SavedModel<TestModel>(1, new TestModel { Name = "Test1" }),
                new SavedModel<TestModel>(2, new TestModel { Name = "Test2" })
            };

            var repository = CreateRepository(initialStorage);

            var allItems = repository.FindAll();
            Assert.AreEqual(2, allItems.Count);
            Assert.IsTrue(allItems.Exists(x => x.Id == 1 && x.Data.Name == "Test1"));
            Assert.IsTrue(allItems.Exists(x => x.Id == 2 && x.Data.Name == "Test2"));
        }

        [TestMethod]
        public void Add_ShouldAddNewItemWithGeneratedId()
        {
            var repository = CreateRepository();

            var item = new TestModel { Name = "New Item" };
            var savedModel = repository.Add(item);

            Assert.AreEqual(1, savedModel.Id);
            Assert.AreEqual(item, savedModel.Data);
            Assert.IsTrue(repository.Exists(savedModel.Id));
        }

        [TestMethod]
        public void AddAll_ShouldAddMultipleItems()
        {
            var repository = CreateRepository();

            var items = new List<TestModel>
            {
                new TestModel { Name = "Item1" },
                new TestModel { Name = "Item2" }
            };

            var savedModels = repository.AddAll(items);

            Assert.AreEqual(2, savedModels.Count());
            Assert.IsTrue(savedModels.Any(x => x.Data.Name == "Item1"));
            Assert.IsTrue(savedModels.Any(x => x.Data.Name == "Item2"));
        }

        [TestMethod]
        public void Update_ShouldModifyExistingItem()
        {
            var repository = CreateRepository();
            var initialItem = repository.Add(new TestModel { Name = "Initial" });

            var updatedItem = new TestModel { Name = "Updated" };
            var result = repository.Update(initialItem.Id, updatedItem);

            Assert.AreEqual(initialItem.Id, result.Id);
            Assert.AreEqual(updatedItem.Name, result.Data.Name);
        }

        [TestMethod]
        public void Update_WithNonExistentId_ShouldThrowException()
        {
            var repository = CreateRepository();

            Assert.ThrowsException<ArgumentException>(() => repository.Update(999, new TestModel { Name = "Invalid" }));
        }

        [TestMethod]
        public void Remove_ShouldRemoveExistingItem()
        {
            var repository = CreateRepository();
            var item = repository.Add(new TestModel { Name = "Test Item" });

            var result = repository.Remove(item.Id);

            Assert.IsTrue(result);
            Assert.IsFalse(repository.Exists(item.Id));
        }

        [TestMethod]
        public void Remove_WithNonExistentId_ShouldReturnFalse()
        {
            var repository = CreateRepository();

            var result = repository.Remove(999);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Find_ShouldReturnSavedModelIfExists()
        {
            var repository = CreateRepository();
            var item = repository.Add(new TestModel { Name = "Test Item" });

            var result = repository.Find(item.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Id);
            Assert.AreEqual(item.Data, result.Data);
        }

        [TestMethod]
        public void Find_ShouldReturnNullIfNotExists()
        {
            var repository = CreateRepository();

            var result = repository.Find(999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindAll_ShouldReturnAllItems()
        {
            var repository = CreateRepository(new List<SavedModel<TestModel>>
            {
                new SavedModel<TestModel>(1, new TestModel { Name = "Item1" }),
                new SavedModel<TestModel>(2, new TestModel { Name = "Item2" })
            });

            var allItems = repository.FindAll();
            Assert.AreEqual(2, allItems.Count);
        }

        [TestMethod]
        public void Exists_ShouldReturnTrueForExistingId()
        {
            var repository = CreateRepository();
            var item = repository.Add(new TestModel { Name = "Test Item" });

            Assert.IsTrue(repository.Exists(item.Id));
        }

        [TestMethod]
        public void Exists_ShouldReturnFalseForNonExistentId()
        {
            var repository = CreateRepository();

            Assert.IsFalse(repository.Exists(999));
        }

        [TestMethod]
        public void Count_ShouldReturnCorrectItemCount()
        {
            var repository = CreateRepository(new List<SavedModel<TestModel>>
            {
                new SavedModel<TestModel>(1, new TestModel { Name = "Item1" }),
                new SavedModel<TestModel>(2, new TestModel { Name = "Item2" })
            });

            Assert.AreEqual(2, repository.Count());
        }

        // Private concrete implementation for testing since the repository is abstract
        private class TestRepository<T> : InMemoryBaseRepository<T> where T : class
        {
            public TestRepository(IEnumerable<SavedModel<T>> storage) : base(storage)
            {
            }
        }
    }
}