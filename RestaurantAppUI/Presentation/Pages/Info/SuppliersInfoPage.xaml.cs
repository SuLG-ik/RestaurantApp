using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Presentation.Pages.Info;

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