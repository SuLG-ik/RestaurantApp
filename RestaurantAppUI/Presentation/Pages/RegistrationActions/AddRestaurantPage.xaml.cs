using System.Collections.ObjectModel;
using RestaurantAppUI.Data.Repository;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;
using RestaurantAppUI.Presentation.Formatter;
using RestaurantAppUI.Presentation.Utils;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Presentation.Pages.RegistrationActions;

public partial class AddRestaurantPage : ContentPage
{
    private readonly IMenuItemRepository _menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();

    private readonly IRestaurantMenuItemRepository _restaurantMenuItemRepository =
        ServiceLocator.GetService<IRestaurantMenuItemRepository>();

    private readonly IFormatter _formatter = ServiceLocator.GetService<IFormatter>();

    public List<SavedModel<MenuItem>> MenuItems { get; }
    public ObservableCollection<RestaurantMenuItem> RestaurantMenuItems { get; } = new();

    public AddRestaurantPage()
    {
        MenuItems = _menuItemRepository.FindAll();
        InitializeComponent();
        BindingContext = this;
    }

    private void UpdateMenuItems()
    {
        var menuItems = RestaurantMenuItems.Select(item => MenuItems.First(mi => mi.Id == item.MenuItemId))
            .Select(value => _formatter.Format(value));
        MenuItemsLabel.Text = string.Join("\n", menuItems);
    }

    private void OnAddMenuItemClicked(object sender, EventArgs e)
    {
        if (MenuItemPicker.SelectedIndex >= 0)
        {
            var selectedMenuItem = MenuItems[MenuItemPicker.SelectedIndex];
            RestaurantMenuItems.Add(new RestaurantMenuItem(0,
                selectedMenuItem.Id)); // 0 is a placeholder for RestaurantId
            UpdateMenuItems();
        }
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var builder = new Restaurant.Builder();
        var form = new ValidatedForm(new List<IValidatedFormEntry>
        {
            ValidatedForm.String(NameEntry, value => builder.SetName(value)),
            ValidatedForm.String(AddressEntry, value => builder.SetAddress(value)),
            ValidatedForm.String(PhoneNumberEntry, value => builder.SetPhoneNumber(value)),
            ValidatedForm.String(DirectorEntry, value => builder.SetDirectorFullname(value)),
        });
        if (form.Validate())
        {
            var model = builder.Build();
            var restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
            var savedRestaurant = restaurantRepository.Add(model);

            foreach (var menuItem in RestaurantMenuItems)
            {
                _restaurantMenuItemRepository.Add(new RestaurantMenuItem(savedRestaurant.Id, menuItem.MenuItemId));
            }

            await DisplayAlert("Успех", $"Информация о ресторане {model.Name} была сохранена.", "Ок");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Ошибка валидации", "Все поля должны быть заполнены верно перед сохранением.", "Ок");
        }
    }
}