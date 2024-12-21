using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Formatter;

public class NameableFormatter : BaseFormatter<INameable>
{
    protected override string Format(INameable value)
    {
        return value.Name;
    }
}