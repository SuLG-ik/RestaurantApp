using System.Collections.ObjectModel;
using RestaurantAppUI.Data.Repository;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;
using RestaurantAppUI.Domain.Service;
using RestaurantAppUI.Presentation.Formatter;
using RestaurantAppUI.Presentation.Utils;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Presentation.Pages;

public partial class CreateSalePage : ContentPage
{
    private readonly IRestaurantRepository _restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
    private readonly IMenuItemRepository _menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
    private readonly ISaleService _saleService = ServiceLocator.GetService<ISaleService>();
    private readonly IFormatter _formatter = ServiceLocator.GetService<IFormatter>();
    private readonly IMenuService _menuService = ServiceLocator.GetService<IMenuService>();

    public List<SavedModel<Restaurant>> Restaurants { get; }
    public List<SavedModel<MenuItem>> MenuItems { get; }
    public ObservableCollection<SaleItem> SaleItems { get; } = new();

    public CreateSalePage()
    {
        Restaurants = _restaurantRepository.FindAll();
        InitializeComponent();
        SaleTimePicker.Time = DateTime.Now.TimeOfDay;
        BindingContext = this;
    }

    private void UpdateSaleItems()
    {
        var items =
            SaleItems.Select(item => _formatter.Format(item));
        SaleItemsLabel.Text = string.Join("\n", items);
    }

    private void OnAddSaleItemClicked(object sender, EventArgs e)
    {
        var builder = new SaleItem.Builder();
        var form = new ValidatedForm(
            [
                ValidatedForm.Picker(MenuItemPicker, index => builder.SetMenuItemId(MenuItems[index].Id)),
                ValidatedForm.Int(QuantityEntry, value => builder.SetQuantity(value)),
                ValidatedForm.Decimal(PriceEntry, value => builder.SetPrice(value)),
            ]
        );
        if (!form.Validate()) return;
        var model = builder.Build();
        SaleItems.Add(model);
        UpdateSaleItems();
    }

    private void OnMenuItemSelected(object sender, EventArgs e)
    {
        var picker = MenuItemPicker;
        var index = picker.SelectedIndex;
        if (index == -1) return;
        var menuItem = MenuItems[index];
        PriceEntry.Text = $"{menuItem.Data.Price}";
    }

    private void OnRestaurantSelected(object sender, EventArgs e)
    {
        var picker = RestaurantPicker;
        var index = picker.SelectedIndex;
        if (index == -1) return;
        var restaurant = Restaurants[index];
        UpdateRestaurant(restaurant);
    }

    private void UpdateRestaurant(SavedModel<Restaurant> restaurant)
    {
        SaleItems.Clear();
        UpdateSaleItems();
        var menuItems = _menuService.FindMenuItemsByRestaurantId(restaurant.Id);
        MenuItemPicker.ItemsSource = menuItems.ToList();
        MenuItemPicker.SelectedIndex = -1;
    }

    private async void OnSaveSaleClicked(object sender, EventArgs e)
    {
        var builder = new Sale.Builder();
        var form = new ValidatedForm(
            [
                ValidatedForm.Picker(RestaurantPicker, index => builder.SetRestaurantId(Restaurants[index].Id)),
                ValidatedForm.Runnable(() => builder.SetDate(SaleDatePicker.Date + SaleTimePicker.Time)),
                ValidatedForm.Runnable(() => builder.SetSaleItems(SaleItems),
                    _ => DisplayAlert("Ошибка", "Нет элементов для добавления к заказу", "Ок")),
            ]
        );
        if (form.Validate())
        {
            var sale = builder.Build();
            if (_saleService.AddSale(sale))
            {
                await DisplayAlert("Успех", "Продажа была сохранена.", "Ок");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ошибка", "Недостаточно продуктов в ресторане", "Ок");
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Все поля должны быть заполнены перед сохранением", "Ок");
        }
    }
}