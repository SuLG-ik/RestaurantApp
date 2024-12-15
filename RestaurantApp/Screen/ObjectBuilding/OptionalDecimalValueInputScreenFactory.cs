namespace RestaurantApp.Screen.ObjectBuilding;

public class OptionalDecimalValueInputScreenFactory(
    Func<string> title,
    Action<decimal?> onAction,
    Func<string>? retryMessage
) : IScreenFactory
{
    public OptionalDecimalValueInputScreenFactory(
        Func<string> title,
        Action<decimal?> onAction
    ) : this(title, onAction, () => "Неверный ввод! Повторите попытку")
    {
    }
    
    public OptionalDecimalValueInputScreenFactory(
        string title,
        Action<decimal?> onAction
    ) : this(() => title, onAction, () => "Неверный ввод! Повторите попытку")
    {
    }

    public Screen CreateScreen()
    {
        var titleText = title();
        var retryMessageText = retryMessage?.Invoke();
        return new ConsoleValueInputScreen<decimal?>(
            titleText,
            (console, onRetry) => console.ReadOptionalDecimalUntilValid(titleText, onRetry),
            onAction,
            retryMessageText
        );
    }
}