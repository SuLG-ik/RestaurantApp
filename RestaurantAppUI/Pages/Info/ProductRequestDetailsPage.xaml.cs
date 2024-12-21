using RestaurantAppUI.Formatter;
using RestaurantAppUI.Model;

namespace RestaurantAppUI.Pages.Info;

public partial class ProductRequestDetailsPage : ContentPage
{
    public ProductRequestDetailsPage(SavedModel<ProductRequest> productRequest)
    {
        InitializeComponent();
        var formatter = ServiceLocator.GetService<IFormatter>();
        RestaurantIdLabel.Text = $"Ресторан: {formatter.Format(productRequest.Data.RestaurantId)}";
        RequestDateLabel.Text = $"Дата заявки: {formatter.Format(productRequest.Data.RequestDate)}";
        ProductRequestItemsCollectionView.ItemsSource = productRequest.Data.ProductRequestItems;
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}