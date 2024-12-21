namespace RestaurantAppUI.Presentation.Formatter;

public interface IFormatter
{
    public string Format(object value);
    public bool Supports(object value);
}

public abstract class BaseFormatter<T> : IFormatter
{
    protected abstract string Format(T value);

    public string Format(object value)
    {
        return Format((T)value);
    }

    public virtual bool Supports(object value)
    {
        return value is T;
    }
}