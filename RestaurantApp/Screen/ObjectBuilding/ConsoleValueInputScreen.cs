namespace RestaurantApp.Screen.ObjectBuilding;

internal class ConsoleValueInputScreen<T>(
    string title,
    ConsoleValueInputScreen<T>.ReadValue readValue,
    Action<T> onAction,
    string? retryMessage) : ValueInputScreen
{
    protected override string Title => title;

    protected override bool Read()
    {
        var result = readValue(_console, retryMessage != null ? () => _console.Write($"{retryMessage} "): null);
        try
        {
            onAction(result);
            return true;
        }
        catch (ValidationException _)
        {
            if (retryMessage != null) _console.WriteLine(retryMessage);
            return false;
        }
    }

    internal delegate T ReadValue(IConsole console, Action? onRetry);
}