namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class InfoPage : ContentPage
{
    public InfoPage()
    {
        InitializeComponent();
    }

    private async void OnSuppliersClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SuppliersInfoPage());
    }

    private async void OnProductsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductsInfoPage());
    }

    private async void OnRestaurantsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RestaurantsInfoPage());
    }

    private async void OnMenuItemsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuItemsInfoPage());
    }

    private async void OnProductRequestsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductRequestsInfoPage());
    }

    private async void OnSalesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SalesInfoPage());
    }
}