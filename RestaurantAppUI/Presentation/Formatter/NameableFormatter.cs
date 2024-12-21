using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Presentation.Formatter;

public class NameableFormatter : BaseFormatter<INameable>
{
    protected override string Format(INameable value)
    {
        return value.Name;
    }
}