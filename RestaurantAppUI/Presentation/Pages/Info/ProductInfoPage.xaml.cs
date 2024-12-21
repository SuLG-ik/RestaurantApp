
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class ProductsInfoPage : ContentPage
{
    public ProductsInfoPage()
    {
        InitializeComponent();
        var productsRepository = ServiceLocator.GetService<IProductRepository>();
        var products = productsRepository.FindAll();
        ProductsCollectionView.ItemsSource = products;
    }
}