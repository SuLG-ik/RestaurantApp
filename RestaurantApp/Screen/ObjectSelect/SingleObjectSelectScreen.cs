using RestaurantApp.Model;
using RestaurantApp.Repository;

namespace RestaurantApp.Screen.ObjectSelect;

public class SingleObjectSelectScreen<T>(
    string title,
    IRepository<T> repository,
    Action<SavedModel<T>> onComplete,
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
        _console.Write("Введите номер объекта: ");
        var id = _console.ReadIntUntilValid("modelId", onRetry: RetryMessage);
        OnSelectId(id);
    }

    private void WriteHeader()
    {
        _console.WriteLine($"---{title}---");
        _console.WriteLine("Единственный выбор объекта");
    }

    private void WriteObjects()
    {
        var objects = repository.FindAll();
        foreach (var model in objects)
        {
            _console.Write($"{model.Id}. ");
            _console.WriteLine(model);
        }
    }

    private void OnSelectId(int id)
    {
        var selectedObject = repository.Find(id);
        if (selectedObject == null)
        {
            _console.WriteLine($"Объект под номером {id} не существует.");
            return;
        }

        _console.WriteLine($"Объект под номером {id} выбран");
        onComplete?.Invoke(selectedObject);
        Navigator?.Back();
    }

    private void RetryMessage()
    {
        _console.Write("Введите номер объекта: ");
    }
}