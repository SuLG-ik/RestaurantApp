namespace RestaurantAppUI.Presentation.Formatter;

public class DateTimeFormatter: BaseFormatter<DateTime>
{
    protected override string Format(DateTime value)
    {
        return value.ToString("dd.MM.yyyy HH:mm:ss");
    }
}