namespace RestaurantApp.Screen;

public abstract class MenuOptionsScreen<T> : Screen where T : struct, Enum
{
    public abstract string? HeaderMessage { get; }

    public virtual string? AskOptionMessage => "Выберите действие: ";

    public virtual string? RetryMessage => "Такого пункта меню нет! Попробуйте снова";

    public abstract Dictionary<T, MenuOption> Options { get; }

    protected IConsole _console;

    protected override void Create()
    {
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        ShowMenu();
        var option = ReadOption();
        RunAction(option);
    }

    private void ShowMenu()
    {
        WriteLineIfAvailable(HeaderMessage);
        ShowOptions();
    }

    private void ShowOptions()
    {
        foreach (var (option, menu) in Options)
        {
            WriteLineIfAvailable($"{Convert.ToInt32(option)}. {menu.Title}");
        }
    }

    private void WriteLineIfAvailable(string? text)
    {
        if (text != null)
        {
            _console.WriteLine(text);
        }
    }

    private void WriteIfAvailable(string? text)
    {
        if (text != null)
        {
            _console.Write(text);
        }
    }

    private T ReadOption()
    {
        WriteIfAvailable(AskOptionMessage);
        return _console.ReadEnumUntilValid<T>(onRetry: RetryMessage != null ? OnRetry : null);
    }

    private void OnRetry()
    {
        WriteIfAvailable($"{RetryMessage}: ");
    }

    private void RunAction(T option)
    {
        var exists = Options.TryGetValue(option, out var menuOption);
        if (!exists)
        {
            WriteLineIfAvailable(RetryMessage);
            return;
        }

        menuOption.Action();
    }
}

public readonly struct MenuOption(string title, Action action)
{
    public string Title { get; } = title;
    public Action Action { get; } = action;
}