using System.Collections;

namespace RestaurantAppUI.Formatter;

public class EnumerableFormatter(IFormatter parent) : BaseFormatter<IEnumerable>
{
    protected override string Format(IEnumerable value)
    {
        var formattedItems = from object? item in value select parent.Format(item);
        var joinedItems =  string.Join("; ", formattedItems);
        return $"[ {joinedItems} ]";
    }

    public override bool Supports(object value)
    {
        return value is IEnumerable and not string;
    }
}