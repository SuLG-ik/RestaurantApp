using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Formatter;

public class SavedModelFormatter(IFormatter generalFormatter) : IFormatter
{
    public string Format(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var model = (ISavedModel<object>)value;
        return $"{generalFormatter.Format(model.Data)} ({model.Id})";
    }

    public bool Supports(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value is ISavedModel<object>;
    }
}