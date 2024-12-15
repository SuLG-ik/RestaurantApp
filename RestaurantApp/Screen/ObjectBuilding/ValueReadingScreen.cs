namespace RestaurantApp.Screen.ObjectBuilding;

public class ValueReadingScreen(string title, Action<string> onValueInput) : Screen
{
    protected IConsole _console;

    protected override void Create()
    {
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        _console.Write($"{title}: ");
        var result = _console.ReadString(title);
        onValueInput(result);
        Navigator?.Back();
    }
}