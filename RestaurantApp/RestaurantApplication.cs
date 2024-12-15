using RestaurantApp.Formatter;

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
        Navigator = navigator;
    }

    public override void Destroy()
    {
        base.Destroy();
        repositories.Destroy();
        ServiceLocator.Reset();
    }
}