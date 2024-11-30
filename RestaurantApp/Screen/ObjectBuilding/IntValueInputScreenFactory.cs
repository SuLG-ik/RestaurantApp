namespace RestaurantApp.Screen.ObjectBuilding;

public class IntValueInputScreenFactory(
    string title,
    Action<int> onAction,
    string? retryMessage = "Неверный ввод! Повторие попытку"
) : IScreenFactory
{
    public Screen CreateScreen()
    {
        return new ConsoleValueInputScreen<int>(
            title,
            (console, onRetry) => console.ReadIntUntilValid(title, onRetry),
            onAction,
            retryMessage
        );
    }
}