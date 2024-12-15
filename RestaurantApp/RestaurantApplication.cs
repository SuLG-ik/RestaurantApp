using RestaurantApp.Formatter;
using RestaurantApp.Repository;
using RestaurantApp.Service;

namespace RestaurantApp;

public class RestaurantApplication(INavigator<Screen.Screen> navigator, IRepositories repositories)
    : Application
{
    public override INavigator<Screen.Screen>? Navigator { get; protected set; }

    public override void Create()
    {
        var formatter = DelegatingFormatter.Default();
        ServiceLocator.Register<IFormatter>(formatter);
        ServiceLocator.Register<IConsole>(new SystemConsole(formatter));
        repositories.Initialize();
        ServiceLocator.Register<IProductsService>(
            new LocalProductService(
                ServiceLocator.GetService<IProductRepository>(),
                ServiceLocator.GetService<IProductRequestRepository>(),
                ServiceLocator.GetService<IProductDeductionRepository>())
        );
        ServiceLocator.Register<ISaleService>(
            new LocalSaleService(
                ServiceLocator.GetService<ISaleRepository>(),
                ServiceLocator.GetService<IProductDeductionRepository>(),
                ServiceLocator.GetService<IMenuItemRepository>(),
                ServiceLocator.GetService<IProductsService>())
        );
        Navigator = navigator;
    }

    public override void Destroy()
    {
        base.Destroy();
        repositories.Destroy();
        ServiceLocator.Reset();
    }
}