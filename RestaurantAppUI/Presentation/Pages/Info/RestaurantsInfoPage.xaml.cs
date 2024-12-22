using RestaurantApp;
using RestaurantApp.Domain.Repository;

namespace RestaurantAppUI.Presentation.Pages.Info;

public partial class RestaurantsInfoPage : ContentPage
{
    public RestaurantsInfoPage()
    {
        InitializeComponent();
        var restaurantRepository = ServiceLocator.GetService<IRestaurantRepository>();
        var restaurants = restaurantRepository.FindAll();
        RestaurantsCollectionView.ItemsSource = restaurants;
    }
}