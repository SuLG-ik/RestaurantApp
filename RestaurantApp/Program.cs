using RestaurantApp.Screen.Main;

namespace RestaurantApp;

public abstract class Program
{
    private static void Main()
    {
        var navigator = new StackNavigator<Screen.Screen>(new MainScreen());
        var application = new RestaurantApplication(navigator, new LocalRepositories());
        application.Create();
        application.Run();
        application.Destroy();
    }
}