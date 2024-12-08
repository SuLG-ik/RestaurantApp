using System.Globalization;

namespace RestaurantApp.Screen.ObjectBuilding;

public class DateTimeValueInputScreenFactory(
    string title,
    Action<DateTime> onAction,
    string format = "dd.MM.yyyy HH:mm",
    string? retryMessage = "Неверный ввод! Повторие попытку"
) : IScreenFactory
{
    public Screen CreateScreen()
    {
        return new ConsoleValueInputScreen<DateTime>(
            title + $" (Формат ввода {format}). Пустое поле – сейчас",
            ReadDateTime,
            onAction,
            retryMessage
        );
    }

    private DateTime ReadDateTime(IConsole console, Action? onRetry)
    {
        while (true)
        {
            var input = console.ReadStringUntilValid(title, onRetry);
            if (input == "")
            {
                return DateTime.Now;
            }

            if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            onRetry?.Invoke();
        }
    }
}