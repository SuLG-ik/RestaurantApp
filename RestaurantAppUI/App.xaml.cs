using RestaurantApp;
using RestaurantApp.Data.Repository;
using RestaurantApp.Data.Service;
using RestaurantApp.Domain.Repository;
using RestaurantApp.Domain.Service;
using RestaurantAppUI.Data.Storage;
using RestaurantAppUI.Presentation.Formatter;

namespace RestaurantAppUI;

public partial class App : Application
{
    private readonly IRepositories _repositories = new LocalRepositories(new MauiStorageFactory());

    public App()
    {
        Init();
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
        window.Destroying += (_, _) => Destroy();
        return window;
    }

    private void Init()
    {
        _repositories.Initialize();
        ServiceLocator.Register<IFormatter>(DelegatingFormatter.Default());
        ServiceLocator.Register<IProductsService>(
            new LocalProductService(
                ServiceLocator.GetService<IProductRepository>(),
                ServiceLocator.GetService<IProductRequestRepository>(),
                ServiceLocator.GetService<IProductDeductionRepository>(),
                ServiceLocator.GetService<IRestaurantMenuItemRepository>(),
                ServiceLocator.GetService<IMenuItemRepository>()
            ));
        ServiceLocator.Register<ISaleService>(
            new LocalSaleService(
                ServiceLocator.GetService<ISaleRepository>(),
                ServiceLocator.GetService<IProductDeductionRepository>(),
                ServiceLocator.GetService<IMenuItemRepository>(),
                ServiceLocator.GetService<IProductsService>())
        );
        ServiceLocator.Register<IMenuService>(
            new LocalMenuService(
                ServiceLocator.GetService<IRestaurantMenuItemRepository>(),
                ServiceLocator.GetService<IMenuItemRepository>(),
                ServiceLocator.GetService<IProductRepository>()
            ));
    }

    private void Destroy()
    {
        _repositories.Destroy();
        ServiceLocator.Reset();
    }
}