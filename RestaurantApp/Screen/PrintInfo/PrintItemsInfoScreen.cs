namespace RestaurantApp.Screen.PrintInfo;

public class PrintItemsInfoScreen<T>(IEnumerable<T> items, string? title = null) : Screen where T : class
{
    private IConsole _console;

    protected override void Create()
    {
        base.Create();
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        if (title != null)
        {
            _console.WriteLine($"{title}");
        }

        if (!items.Any())
        {
            _console.WriteLine("Список пуст!");
        }

        foreach (var item in items)
        {
            _console.WriteLine(item);
        }

        Navigator?.Back();
    }
}