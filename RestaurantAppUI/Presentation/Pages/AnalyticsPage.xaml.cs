using RestaurantApp;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;
using RestaurantApp.Domain.Service;

namespace RestaurantAppUI.Presentation.Pages;

public partial class AnalyticsPage : ContentPage
{
    private readonly IRestaurantRepository _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
    private readonly ISaleService _saleService = ServiceLocator.GetService<ISaleService>();
    private readonly IProductsService _productsService = ServiceLocator.GetService<IProductsService>();
    private readonly IProductRepository _productRepository = ServiceLocator.GetService<IProductRepository>();

    private List<SavedModel<Product>> _products;
    public List<SavedModel<Restaurant>> Restaurants { get; }

    public AnalyticsPage()
    {
        Restaurants = _restaurantRepository.FindAll();
        _products = _productRepository.FindAll();
        InitializeComponent();
        BindingContext = this;
    }

    private void OnRestaurantSelected(object sender, EventArgs e)
    {
        if (RestaurantPicker.SelectedIndex >= 0)
        {
            var selectedRestaurant = Restaurants[RestaurantPicker.SelectedIndex];
            UpdateRevenue(selectedRestaurant.Id);
            UpdateProducts(selectedRestaurant.Id);
        }
    }

    private void UpdateRevenue(int restaurantId)
    {
        var revenue = _saleService.CalculateSalesRevenue(restaurantId);
        RevenueLabel.Text = $"Выручка: {revenue}";
    }

    private void UpdateProducts(int restaurantId)
    {
        var entries = new List<ProductEntry>();
        var requiredProductIds = _productsService.FindRequiredInMenuProducts(restaurantId).Select(value => value.Id)
            .ToList();
        foreach (var product in _products)
        {
            var quantity = _productsService.CalculateProductsQuantityInRestaurant(restaurantId, product.Id);
            var isNecessary = requiredProductIds.Contains(product.Id);
            entries.Add(new ProductEntry(product, quantity, isNecessary));
        }

        ProductsCollectionView.ItemsSource = entries.OrderByDescending(entry => entry.Quantity);
    }
}

public class ProductEntry(SavedModel<Product> product, decimal quantity, bool isNecessary)
{
    public SavedModel<Product> Product => product;
    public decimal Quantity => quantity;
    public bool IsNecessary => isNecessary;
}