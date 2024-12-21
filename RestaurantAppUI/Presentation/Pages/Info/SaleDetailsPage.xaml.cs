using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Presentation.Formatter;

namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class SaleDetailsPage : ContentPage
{
    public SaleDetailsPage(SavedModel<Sale> sale)
    {
        InitializeComponent();
        var formatter = ServiceLocator.GetService<IFormatter>();
        RestaurantIdLabel.Text = $"Restaurant: {formatter.Format(sale.Data.RestaurantId)}";
        DateLabel.Text = $"Date: {formatter.Format(sale.Data.Date)}";
        SaleItemsCollectionView.ItemsSource = sale.Data.SaleItems;
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}