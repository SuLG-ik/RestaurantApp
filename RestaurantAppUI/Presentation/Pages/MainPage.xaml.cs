using RestaurantAppUI.Presentation.Pages.Info;
using RestaurantAppUI.Presentation.Pages.RegistrationActions;

namespace RestaurantAppUI.Presentation.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAddSaleClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateSalePage());
    }

    private async void OnAddProductsRequestClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateProductRequestPage());
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
        await Navigation.PushAsync(new AnalyticsPage());
    }
}