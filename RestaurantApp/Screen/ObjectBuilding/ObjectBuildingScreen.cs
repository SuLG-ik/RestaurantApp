namespace RestaurantApp.Screen.ObjectBuilding;

public abstract class ObjectBuildingScreen : Screen
{
    protected abstract string? HeaderMessage { get; }
    protected virtual string? CompleteMessage => null; 
    protected abstract IScreenFactory[] ScreenFactories { get; }

    protected IConsole _console;

    private int _currentScreen;

    protected override void Create()
    {
        _console = ServiceLocator.GetService<IConsole>();
        if (HeaderMessage != null) _console.WriteLine(HeaderMessage);
    }

    protected override void Resume()
    {
        if (_currentScreen >= ScreenFactories.Length)
        {
            Navigator?.Back();
            Complete();
            return;
        }

        var screen = ScreenFactories[_currentScreen].CreateScreen();
        Navigator?.NavigateTo(screen);
        _currentScreen += 1;
    }

    protected abstract void Complete();

    protected override void Destroy()
    {
        if (CompleteMessage != null) _console.WriteLine(CompleteMessage);
    }
}