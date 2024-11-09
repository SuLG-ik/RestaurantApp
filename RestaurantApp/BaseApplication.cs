using ConsoleApp1;

namespace RestaurantApp;


public class BaseApplication(INavigator<Screen.Screen> navigator) : Application
{
    public override INavigator<Screen.Screen>? Navigator { get; protected set; }

    public override void Create()
    {
        ServiceLocator.Register<IConsole>(new SystemConsole());
        Navigator = navigator;
    }

    public override void Destroy()
    {
        base.Destroy();
        ServiceLocator.Reset();
    }
}