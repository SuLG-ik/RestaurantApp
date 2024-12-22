using RestaurantApp;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class SalesInfoPage : ContentPage
{
    public SalesInfoPage()
    {
        InitializeComponent();
        var saleRepository = ServiceLocator.GetService<ISaleRepository>();
        var sales = saleRepository.FindAll();
        SalesCollectionView.ItemsSource = sales;
    }

    private async void OnMoreInfoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is SavedModel<Sale> sale)
        {
            await Navigation.PushModalAsync(new SaleDetailsPage(sale));
        }
    }
}