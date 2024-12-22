using System.Collections.ObjectModel;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;
using RestaurantAppUI.Domain.Service;
using RestaurantAppUI.Presentation.Formatter;
using RestaurantAppUI.Presentation.Utils;

namespace RestaurantAppUI.Presentation.Pages;

public partial class CreateProductRequestPage : ContentPage
{
    private readonly IRestaurantRepository _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
    private readonly IProductRepository _productRepository = ServiceLocator.GetService<IProductRepository>();
    private readonly IProductsService _productsService = ServiceLocator.GetService<IProductsService>();
    private readonly IMenuService _menuService = ServiceLocator.GetService<IMenuService>();
    private readonly IFormatter _formatter = ServiceLocator.GetService<IFormatter>();
    private List<SavedModel<Product>> _products = [];
    public List<SavedModel<Restaurant>> Restaurants { get; }
    public ObservableCollection<ProductRequestItem> ProductRequestItems { get; } = new();

    public CreateProductRequestPage()
    {
        Restaurants = _restaurantRepository.FindAll();
        InitializeComponent();
        RequestTimePicker.Time = DateTime.Now.TimeOfDay;
        BindingContext = this;
    }

    private void UpdateProductRequestItems()
    {
        var productRequestItems = ProductRequestItems.Select(item => _formatter.Format(item));
        ProductRequestItemsLabel.Text = string.Join("\n", productRequestItems);
    }

    private void OnAddProductClicked(object sender, EventArgs e)
    {
        if (ProductPicker.SelectedIndex >= 0 && decimal.TryParse(QuantityEntry.Text, out var quantity))
        {
            var selectedProduct = _products[ProductPicker.SelectedIndex];
            var productRequestItem = new ProductRequestItem.Builder()
                .SetProductId(selectedProduct.Id)
                .SetQuantity(quantity)
                .Build();

            if (_productsService.IsProductRequestItemQuantityAvailable(productRequestItem,
                    ProductRequestItems.ToList()))
            {
                ProductRequestItems.Add(productRequestItem);
                UpdateProductRequestItems();
            }
            else
            {
                var availableQuantity =
                    _productsService.CalculateProductRequestItemQuantityAvailable(selectedProduct.Id,
                        ProductRequestItems);
                DisplayAlert("Ошибка", $"Недостаточное количество продукта. Доступное количество: {availableQuantity}",
                    "Ок");
            }
        }
    }


    private async void OnSaveRequestClicked(object sender, EventArgs e)
    {
        var builder = new ProductRequest.Builder();
        var form = new ValidatedForm(
        [
            ValidatedForm.Picker(RestaurantPicker, index => builder.SetRestaurantId(Restaurants[index].Id)),
            ValidatedForm.Runnable(() => builder.SetRequestDate(RequestDatePicker.Date + RequestTimePicker.Time)),
            ValidatedForm.Runnable(() => builder.SetProductRequestItems(ProductRequestItems),
                _ => DisplayAlert("Ошибка", "Нет элементов для добавления к заявке", "Ок")),
        ]);
        if (form.Validate())
        {
            var productRequest = builder.Build();
            _productsService.AddProductRequest(productRequest);

            await DisplayAlert("Успех", "Запрос на продукт был сохранен.", "Ок");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Ошибка", "Все поля должны быть заполнены перед сохранением.", "Ок");
        }
    }

    private void OnRestaurantSelect(object? sender, EventArgs e)
    {
        var index = RestaurantPicker.SelectedIndex;
        if (index == -1) return;
        var restaurant = Restaurants[index];
        _products = _menuService.FindRequiredProductsByRestaurantId(restaurant.Id).ToList();
        ProductRequestItems.Clear();
        ProductPicker.ItemsSource = _products;
        UpdateProductRequestItems();
    }
}