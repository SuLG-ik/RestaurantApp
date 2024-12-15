using RestaurantApp.Model;
using RestaurantApp.Repository;

namespace RestaurantApp.Screen.ObjectSelect;

public class MultipleObjectSelectScreen<T>(
    string title,
    IRepository<T> repository,
    Action<List<SavedModel<T>>> onComplete,
    Action onFailed
) : Screen where T : class
{
    private readonly List<SavedModel<T>> _selectedObjects = [];
    private IConsole _console;

    protected override void Create()
    {
        base.Create();
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        if (repository.Count() == 0)
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
        _console.Write("Введите номер объекта или 0: ");
        var id = _console.ReadIntUntilValid("modelId", onRetry: RetryMessage);
        if (id == 0)
        {
            OnExit();
        }
        else
        {
            OnSelectId(id);
        }
    }

    private void WriteHeader()
    {
        _console.WriteLine($"---{title}---");
        _console.WriteLine("Множественный выбор объектов. 0 – закончить выбор");
    }

    private void WriteObjects()
    {
        var objects = repository.FindAll();
        foreach (var model in objects)
        {
            var isSelected = _selectedObjects.Contains(model);
            _console.Write($"{FormatNumber(model.Id, isSelected)}. ");
            _console.WriteLine(model);
        }
    }

    private void OnSelectId(int id)
    {
        var selectedObject = repository.Find(id);
        if (selectedObject == null)
        {
            _console.WriteLine("Объект под номером {id} не существует.");
            return;
        }

        if (_selectedObjects.Remove(selectedObject))
        {
            _console.WriteLine("Объект под номером {id} больше не выбран");
            return;
        }

        _selectedObjects.Add(selectedObject);
        _console.WriteLine("Объект под номером {id} выбран");
    }

    private void OnExit()
    {
        WriteExitMessage();

        onComplete(_selectedObjects);
        Navigator?.Back();
    }

    private void WriteExitMessage()
    {
        if (_selectedObjects.Count == 0)
        {
            _console.WriteLine("Ни одного объекта не выбрано");
        }
        else
        {
            _console.WriteLine("Выбранные объекты:");
            foreach (var selectedObject in _selectedObjects)
            {
                _console.WriteLine(selectedObject);
            }
        }
    }

    private void RetryMessage()
    {
        _console.Write("Введите номер объекта или 0: ");
    }

    private static string FormatNumber(int number, bool isSelected)
    {
        return isSelected ? $"({number})" : $"{number}";
    }
}