using RestaurantApp;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class ProductRequestsInfoPage : ContentPage
{
    public ProductRequestsInfoPage()
    {
        InitializeComponent();
        var productRequestRepository = ServiceLocator.GetService<IProductRequestRepository>();
        var productRequests = productRequestRepository.FindAll();
        ProductRequestsCollectionView.ItemsSource = productRequests;
    }

    private async void OnMoreInfoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is SavedModel<ProductRequest> productRequest)
        {
            await Navigation.PushModalAsync(new ProductRequestDetailsPage(productRequest));
        }
    }
}