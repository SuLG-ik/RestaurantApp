namespace RestaurantApp.Screen.ObjectBuilding;

public class MultipleItemsBuildingScreen<T>(
    string title,
    IParametrizedScreenFactory<MultipleItemsParams<T>> factory,
    Action<List<T>> onComplete,
    bool required
) : Screen where T : class
{
    private readonly List<T> _items = [];
    private IConsole _console;

    protected override void Create()
    {
        base.Create();
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        WriteHeader();
        WriteObjects();
        UserInput();
    }

    private void UserInput()
    {
        if (_items.Count > 0 || !required)
        {
            _console.Write("Добавить ещё объект в список? 0 – нет, 1 – да:");
            var variant = _console.ReadIntUntilValid("variant", onRetry: RetryMessage);
            if (variant == 0)
            {
                OnExit();
            }
            else
            {
                OnContinue();
            }
        }
        else
        {
            OnContinue();
        }
    }

    private void WriteHeader()
    {
        _console.WriteLine($"---{title}---");
    }

    private void OnContinue()
    {
        var screen = factory.CreateScreen(new MultipleItemsParams<T>(OnItemAdded, _items));
        Navigator?.NavigateTo(screen);
    }

    private void WriteObjects()
    {
        _console.WriteLine($"Текущие добавленные объекты");
        foreach (var item in _items)
        {
            _console.WriteLine(item);
        }
    }

    private void OnExit()
    {
        WriteExitMessage();

        onComplete(_items);
        Navigator?.Back();
    }

    private void OnItemAdded(T item)
    {
        _items.Add(item);
    }

    private void WriteExitMessage()
    {
        if (_items.Count == 0)
        {
            _console.WriteLine("Ни одного объекта не добавлено");
        }
        else
        {
            _console.WriteLine("Добавленные объекты:");
            foreach (var selectedObject in _items)
            {
                _console.WriteLine(selectedObject);
            }
        }
    }

    private void RetryMessage()
    {
        _console.Write("Введите 0 – не добавлять, 1 – добавить: ");
    }
}