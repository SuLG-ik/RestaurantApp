using RestaurantApp.Model;

namespace RestaurantApp.Formatter;

public class SavedModelFormatter(IFormatter generalFormatter) : IFormatter
{
    public string Format(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var model = (ISavedModel<object>)value;
        return $"Созранённый объект: ID: {model.Id}, Данные: {generalFormatter.Format(model.Data)}";
    }

    public bool Supports(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value is ISavedModel<object>;
    }
}