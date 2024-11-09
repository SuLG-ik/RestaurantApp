namespace RestaurantApp;

public abstract class Application
{
    public abstract INavigator<Screen.Screen>? Navigator { get; protected set; }
    private Screen.Screen? _currentScreen;

    public abstract void Create();

    public void Run()
    {
        while (true)
        {
            if (!RunLoop()) return;
        }
    }

    private bool RunLoop()
    {
        var navigator = Navigator;
        if (navigator == null) return false;
        var newScreen = navigator.CurrentScreen;
        if (newScreen == null)
        {
            _currentScreen?.BaseDestroy();
            return false;
        }

        if (_currentScreen == newScreen)
        {
            newScreen?.Display();
        }
        else
        {
            newScreen.BaseCreate(navigator);
            _currentScreen?.BaseDestroy();
            _currentScreen = newScreen;
        }

        return true;
    }

    public virtual void Destroy()
    {
        Navigator = null;
        _currentScreen = null;
    }
}