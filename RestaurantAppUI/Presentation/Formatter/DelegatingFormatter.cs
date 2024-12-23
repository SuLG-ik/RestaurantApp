namespace RestaurantAppUI.Presentation.Formatter;

public class DelegatingFormatter : IFormatter
{
    private readonly List<IFormatter> _formatters;
    private readonly Dictionary<Type, IFormatter> _cache = new();

    public DelegatingFormatter(List<Func<IFormatter, IFormatter>> factories)
    {
        _formatters = factories.Select((item) => item(this)).ToList();
    }

    public string Format(object value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var type = value.GetType();

        if (_cache.TryGetValue(type, out var formatter)) return formatter.Format(value);
        formatter = _formatters.FirstOrDefault(f => f.Supports(value))
                    ?? throw new InvalidOperationException($"No formatter found for type {type}");
        _cache[type] = formatter;

        return formatter.Format(value);
    }

    public bool Supports(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return _formatters.Any(f => f.Supports(value));
    }

    public static DelegatingFormatter Default()
    {
        var formatter = new DelegatingFormatter(
            [
                parent => new SavedModelFormatter(parent),
                parent => new EnumerableFormatter(parent),
                _ => new NameableFormatter(),
                parent => new ProductRequestFormatter(parent),
                parent => new ProductEditingFormatter(parent),
                parent => new IngredientFormatter(parent),
                parent => new ProductRequestItemFormatter(parent),
                _ => new ProductGroupFormatter(),
                _ => new UnitFormatter(),
                _ => new DateTimeFormatter(),
                parent => new SaleFormatter(parent),
                _ => new SaleItemFormatter(),
                _ => new ToStringFormatter(),
            ]
        );
        return formatter;
    }
}