using System.Text;
using RestaurantApp.Formatter;

namespace RestaurantApp.Screen.ObjectBuilding;

public class EnumValueInputScreenFactory<T>(
    string title,
    Action<T> onAction,
    string? retryMessage = "Неверный ввод! Повторие попытку")
    : IScreenFactory where T : struct, Enum
{
    public Screen CreateScreen()
    {
        return new ConsoleValueInputScreen<T>(
            title + BuildVariants(),
            (console, onRetry) => console.ReadEnumUntilValid<T>(title, onRetry),
            onAction,
            retryMessage
        );
    }

    private static string BuildVariants()
    {
        var formatter = ServiceLocator.GetService<IFormatter>();
        var values = Enum.GetValues<T>();
        var variants = new StringBuilder(" ");
        for (var i = 0; i < values.Length; i++)
        {
            variants.Append($"{i} - {formatter.Format(values[i])}");
            if (i != values.Length - 1)
            {
                variants.Append(", ");
            }
        }
        return variants.ToString();
    }
}