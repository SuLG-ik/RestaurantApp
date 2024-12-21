namespace RestaurantAppUI.Presentation.Formatter;

public class ToStringFormatter : IFormatter
{
    public string Format(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var result = value.ToString();
        ArgumentNullException.ThrowIfNull(result);
        return result;
    }

    public bool Supports(object value)
    {
        return value?.ToString() != null;
    }
}