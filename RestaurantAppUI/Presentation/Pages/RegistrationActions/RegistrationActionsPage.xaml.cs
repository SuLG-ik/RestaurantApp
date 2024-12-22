namespace RestaurantAppUI.Presentation.Pages.RegistrationActions;

public partial class RegistrationActionsPage : ContentPage
{
    public RegistrationActionsPage()
    {
        InitializeComponent();
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddProductPage());
    }

    private async void OnAddSupplierClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddSupplierPage());
    }

    private async void OnAddRestaurantClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddRestaurantPage());
    }

    private async void OnAddMenuItemClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddMenuItemPage());
    }
}