using RestaurantApp.Formatter;
using RestaurantApp.Repository;

namespace RestaurantApp;

public class BaseApplication(INavigator<Screen.Screen> navigator) : Application
{
    public override INavigator<Screen.Screen>? Navigator { get; protected set; }

    public override void Create()
    {
        var formatter = DelegatingFormatter.Default();
        ServiceLocator.Register<IFormatter>(formatter);
        ServiceLocator.Register<IConsole>(new SystemConsole(formatter));
        ServiceLocator.Register<ISupplierRepository>(new InMemorySupplierRepository());
        ServiceLocator.Register<IProductRepository>(new InMemoryProductRepository());
        ServiceLocator.Register<IRestaurantRepository>(new InMemoryRestaurantRepository());
        Navigator = navigator;
    }

    public override void Destroy()
    {
        base.Destroy();
        ServiceLocator.Reset();
    }
}