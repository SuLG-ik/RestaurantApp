namespace RestaurantApp.Screen.ObjectBuilding;

public class DecimalValueInputScreenFactory(
    string title,
    Action<decimal> onAction,
    string? retryMessage = "Неверный ввод! Повторие попытку"
) : IScreenFactory
{
    public Screen CreateScreen()
    {
        return new ConsoleValueInputScreen<decimal>(
            title,
            (console, onRetry) => console.ReadDecimalUntilValid(title, onRetry),
            onAction,
            retryMessage
        );
    }
}