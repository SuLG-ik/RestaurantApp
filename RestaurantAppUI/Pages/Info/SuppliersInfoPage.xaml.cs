using RestaurantAppUI.Model;
using RestaurantAppUI.Repository;

namespace RestaurantAppUI.Pages.Info;

public partial class SuppliersInfoPage : ContentPage
{
    public SuppliersInfoPage()
    {
        InitializeComponent();
        var suppliersRepository = ServiceLocator.GetService<ISupplierRepository>();
        var supplier = suppliersRepository.FindAll();
        SuppliersCollectionView.ItemsSource = supplier;
    }
}