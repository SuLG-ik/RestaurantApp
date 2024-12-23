using RestaurantApp.Domain.Model;

namespace RestaurantAppUI.Presentation.Formatter;

public class UnitFormatter: BaseFormatter<Unit>
{
    protected override string Format(Unit value)
    {
        return value switch
        {
            Unit.Kg => "кг.",
            Unit.Liter => "л.",
            Unit.Item => "штук.",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}