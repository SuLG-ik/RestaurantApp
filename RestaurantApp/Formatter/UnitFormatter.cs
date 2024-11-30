using RestaurantApp.Model;

namespace RestaurantApp.Formatter;

public class UnitFormatter: BaseFormatter<Unit>
{
    protected override string Format(Unit value)
    {
        return value switch
        {
            Unit.Gram => "граммы",
            Unit.Milliliter => "миллилитры",
            Unit.Item => "штука",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}