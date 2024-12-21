using RestaurantAppUI.Domain;

namespace RestaurantAppUI.Presentation.Utils;

public interface IValidatedFormEntry<out T>
{
    public bool Validate();
}

public class ValidatedFormEntry<T>(
    InputView input,
    Func<string, T> validator,
    Action<T> setter,
    Action<ValidationException>? onError) : IValidatedFormEntry<T>
{
    public bool Validate()
    {
        try
        {
            var value = validator.Invoke(input.Text);
            setter.Invoke(value);
            if (onError == null)
            {
                input.BackgroundColor = Colors.Transparent;
            }

            return true;
        }
        catch (ValidationException ex)
        {
            if (onError == null)
            {
                input.BackgroundColor = Colors.Red;
            }
            else
            {
                onError(ex);
            }

            return false;
        }
    }
};

public class ValidatedForm(IEnumerable<IValidatedFormEntry<object>> entries)
{
    public bool Validate()
    {
        return entries.Aggregate(true, (current, entry) => entry.Validate() && current);
    }

    public static IValidatedFormEntry<string> String(
        InputView input,
        Action<string> setter,
        Action<ValidationException>? onError = null)
    {
        return new ValidatedFormEntry<string>(input, (value) => value, setter, onError);
    }

    public static IValidatedFormEntry<decimal> Decimal(
        InputView input,
        Action<decimal> setter,
        Action<ValidationException>? onError = null)
    {
        return new ValidatedFormEntry<decimal>(input, Domain.Validator.RequireDecimal, setter, onError);
    }

    public static IValidatedFormEntry<int> Int(
        InputView input,
        Action<int> setter,
        Action<ValidationException>? onError = null)
    {
        return new ValidatedFormEntry<int>(input, Domain.Validator.RequireInt, setter, onError);
    }
}