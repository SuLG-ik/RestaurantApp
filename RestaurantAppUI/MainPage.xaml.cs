using RestaurantAppUI.Pages;
using RestaurantAppUI.Pages.Info;
using RestaurantAppUI.Pages.RegistrationActions;

namespace RestaurantAppUI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAddSaleClicked(object sender, EventArgs e)
    {
    }

    private async void OnAddProductsRequestClicked(object sender, EventArgs e)
    {
    }

    private async void OnRegistrationActionsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationActionsPage());
    }

    private async void OnShowInfoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }

    private async void OnAnalyticsClicked(object sender, EventArgs e)
    {
    }
}