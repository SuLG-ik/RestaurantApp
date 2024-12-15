using RestaurantApp.Model;
using RestaurantApp.Repository;

namespace RestaurantApp.Screen.ObjectSelect;

public class SingleObjectSelectScreen<T>(
    string title,
    Func<IEnumerable<T>> objectsProvider,
    Action<T> onComplete,
    Action onFailed
) : Screen where T : class
{
    private IConsole _console;

    protected override void Create()
    {
        base.Create();
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        var objects = objectsProvider.Invoke();
        if (!objects.Any())
        {
            onFailed();
            Navigator?.Back();
            return;
        }

        WriteHeader();
        WriteObjects();
        UserInput();
    }

    private void UserInput()
    {
        _console.Write("Введите номер объекта: ");
        var number = _console.ReadIntUntilValid("modelNumber", onRetry: RetryMessage);
        OnSelectNumber(number);
    }

    private void WriteHeader()
    {
        _console.WriteLine($"---{title}---");
        _console.WriteLine("Единственный выбор объекта");
    }

    private void WriteObjects()
    {
        var objects = objectsProvider.Invoke().ToList();
        for (var i = 0; i < objects.Count; i++)
        {
            _console.Write($"{i + 1}. ");
            _console.WriteLine(objects[i]);
        }
    }

    private void OnSelectNumber(int number)
    {
        var objects = objectsProvider().ToList();
        if (number - 1 >= objects.Count)
        {
            _console.WriteLine($"Объект под номером {number} не существует.");
            return;
        }
        var selectedObject = objects[number - 1];

        _console.WriteLine($"Объект под номером {number} выбран");
        onComplete?.Invoke(selectedObject);
        Navigator?.Back();
    }

    private void RetryMessage()
    {
        _console.Write("Введите номер объекта: ");
    }
}