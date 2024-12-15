namespace RestaurantApp.Screen.ObjectBuilding;

public class StringValueInputScreenFactory(
    string title,
    Action<string> onAction,
    string? retryMessage = "Неверный ввод! Повторие попытку"
) : IScreenFactory
{
    public Screen CreateScreen()
    {
        return new ConsoleValueInputScreen<string>(
            title,
            (console, onRetry) => console.ReadStringUntilValid(title, onRetry),
            onAction,
            retryMessage
        );
    }
}