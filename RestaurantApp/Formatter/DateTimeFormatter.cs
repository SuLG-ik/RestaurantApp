namespace RestaurantApp.Formatter;

public class DateTimeFormatter: BaseFormatter<DateTime>
{
    protected override string Format(DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm:ss");
    }
}