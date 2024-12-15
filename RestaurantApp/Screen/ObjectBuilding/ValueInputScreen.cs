namespace RestaurantApp.Screen.ObjectBuilding;

internal abstract class ValueInputScreen : Screen
{
    protected IConsole _console;

    protected abstract string Title { get; }

    protected abstract bool Read();

    protected override void Create()
    {
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        _console.Write($"{Title}: ");
        if (Read())
        {
            Navigator?.Back();
        }
    }
}