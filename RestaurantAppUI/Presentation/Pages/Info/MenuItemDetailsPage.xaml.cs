using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Formatter;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class MenuItemDetailsPage : ContentPage
{
    private readonly IFormatter _formatter;

    public MenuItemDetailsPage(SavedModel<MenuItem> menuItem)
    {
        InitializeComponent();
        _formatter = ServiceLocator.GetService<IFormatter>();
        NameLabel.Text = menuItem.Data.Name;
        GroupLabel.Text = _formatter.Format(menuItem.Data.Group);
        PriceLabel.Text = $"Цена: {menuItem.Data.Price}";
        IngredientsLabel.Text = _formatter.Format(menuItem.Data.Ingredients);
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}