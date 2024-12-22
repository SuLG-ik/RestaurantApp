
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantApp.Data.Repository;
using RestaurantApp.Data.Service;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;
using RestaurantApp.Domain.Service;

namespace RestaurantApp.Tests.Service
{
    [TestClass]
    public class LocalSaleServiceTests
    {
        private Mock<ISaleRepository> _mockSaleRepository;
        private Mock<IProductDeductionRepository> _mockProductDeductionRepository;
        private Mock<IMenuItemRepository> _mockMenuItemRepository;
        private Mock<IProductsService> _mockProductsService;

        private LocalSaleService _localSaleService;

        [TestInitialize]
        public void Setup()
        {
            _mockSaleRepository = new Mock<ISaleRepository>();
            _mockProductDeductionRepository = new Mock<IProductDeductionRepository>();
            _mockMenuItemRepository = new Mock<IMenuItemRepository>();
            _mockProductsService = new Mock<IProductsService>();

            _localSaleService = new LocalSaleService(
                _mockSaleRepository.Object,
                _mockProductDeductionRepository.Object,
                _mockMenuItemRepository.Object,
                _mockProductsService.Object
            );
        }

        [TestMethod]
        public void AddSale_ShouldAddSaleSuccessfully_WhenEnoughProductsAvailable()
        {
            var sale = CreateTestSale();
            var menuItems = CreateMenuItems();
            var productIngredients = menuItems.SelectMany(m => m.Data.Ingredients).ToList();

            _mockMenuItemRepository
                .Setup(repo => repo.FindAllByIds(It.IsAny<IEnumerable<int>>()))
                .Returns(menuItems);

            _mockProductsService
                .Setup(service => service.CalculateProductsQuantityInRestaurant(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(10);

            _mockSaleRepository.Setup(repo => repo.Add(It.IsAny<Sale>()));
            _mockProductDeductionRepository.Setup(repo => repo.AddAll(It.IsAny<IEnumerable<ProductDeduction>>()));

            var result = _localSaleService.AddSale(sale);

            Assert.IsTrue(result, "Sale should be added successfully.");

            _mockSaleRepository.Verify(repo => repo.Add(It.Is<Sale>(s => s == sale)), Times.Once);
            
            _mockProductDeductionRepository.Verify(repo => repo.AddAll(It.Is<IEnumerable<ProductDeduction>>(deductions =>
                deductions.All(d => productIngredients.Any(ingredient =>
                    ingredient.ProductId == d.ProductId &&
                    ingredient.Quantity == d.Quantity &&
                    d.RestaurantId == sale.RestaurantId
                ))
            )), Times.Once);
        }

        [TestMethod]
        public void AddSale_ShouldReturnFalse_WhenNotEnoughProductsAvailable()
        {
            // Arrange
            var sale = CreateTestSale();
            var menuItems = CreateMenuItems();

            // Mock menu items in repository
            _mockMenuItemRepository
                .Setup(repo => repo.FindAllByIds(It.IsAny<IEnumerable<int>>()))
                .Returns(menuItems);

            // Mock product quantities (insufficient)
            _mockProductsService
                .Setup(service => service.CalculateProductsQuantityInRestaurant(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(5); // Assume each product has a quantity of 5, not sufficient for all deductions

            // Act
            var result = _localSaleService.AddSale(sale);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CalculateSalesRevenue_ShouldReturnZero_WhenNoSalesExist()
        {
            // Arrange
            _mockSaleRepository
                .Setup(repo => repo.FindAllByRestaurantId(It.IsAny<int>()))
                .Returns(new List<SavedModel<Sale>>()); // No sales

            // Act
            var revenue = _localSaleService.CalculateSalesRevenue(1);

            // Assert
            Assert.AreEqual(0, revenue, "Revenue should be 0 when no sales exist.");
        }

        [TestMethod]
        public void CalculateSalesRevenue_ShouldReturnCorrectRevenue_WhenSalesExist()
        {
            var sales = new List<SavedModel<Sale>>
            {
                new(1, CreateTestSale(100)),
                new(2, CreateTestSale(200))
            };

            _mockSaleRepository
                .Setup(repo => repo.FindAllByRestaurantId(It.IsAny<int>()))
                .Returns(sales);

            var revenue = _localSaleService.CalculateSalesRevenue(1);

            Assert.AreEqual(300, revenue, "Revenue should be the sum of all sales' TotalPrice.");
        }


        private Sale CreateTestSale(decimal totalPrice = 0)
        {
            var builder = new Sale.Builder()
                .SetRestaurantId(1)
                .SetDate(DateTime.Now)
                .AddSaleItems(new List<SaleItem>
                {
                    new SaleItem.Builder()
                        .SetMenuItemId(1)
                        .SetQuantity(2)
                        .SetPrice(totalPrice / 2) // Divide totalPrice evenly between items
                        .Build(),
                });
            return builder.Build();
        }

        private List<SavedModel<MenuItem>> CreateMenuItems()
        {
            return new List<SavedModel<MenuItem>>
            {
                new SavedModel<MenuItem>(1, new MenuItem.Builder()
                    .SetName("Pizza")
                    .SetGroup(MenuItemGroup.SecondCourses)
                    .SetPrice(10)
                    .SetIngredients(new List<Ingredient>
                    {
                        new Ingredient.Builder()
                            .SetProductId(1)
                            .SetQuantity(2)
                            .Build()
                    })
                    .Build()),
                new SavedModel<MenuItem>(2, new MenuItem.Builder()
                    .SetName("Pasta")
                    .SetGroup(MenuItemGroup.FirstCourses)
                    .SetPrice(15)
                    .SetIngredients(new List<Ingredient>
                    {
                        new Ingredient.Builder()
                            .SetProductId(2)
                            .SetQuantity(3)
                            .Build()
                    })
                    .Build())
            };
        }
    }
}