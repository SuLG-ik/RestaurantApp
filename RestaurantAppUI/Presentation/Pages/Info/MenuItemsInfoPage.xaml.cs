using RestaurantAppUI.Data.Repository;
using RestaurantAppUI.Domain.Model;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class MenuItemsInfoPage : ContentPage
{
    public MenuItemsInfoPage()
    {
        InitializeComponent();
        var menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
        var menuItems = menuItemRepository.FindAll();
        MenuItemsCollectionView.ItemsSource = menuItems;
    }

    private async void OnMoreInfoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is SavedModel<MenuItem> menuItem)
        {
            await Navigation.PushModalAsync(new MenuItemDetailsPage(menuItem));
        }
    }
}