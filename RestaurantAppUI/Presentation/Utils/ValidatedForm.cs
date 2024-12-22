using RestaurantAppUI.Domain;

namespace RestaurantAppUI.Presentation.Utils;

public class ValidatedForm
{
    private IList<IValidatedFormEntry> _entries;

    public ValidatedForm(IEnumerable<IValidatedFormEntry> entries)
    {
        _entries = entries.ToList();
    }

    public void AddEntry(IValidatedFormEntry entry)
    {
        _entries.Add(entry);
    }

    public bool Validate()
    {
        return _entries.Aggregate(true, (current, entry) => entry.Validate() && current);
    }

    public static IValidatedFormEntry String(
        InputView input,
        Action<string> setter,
        Action<ValidationException>? onError = null)
    {
        return new InputViewValidatedFormEntry<string>(input, (value) => value, setter, onError);
    }

    public static IValidatedFormEntry Decimal(
        InputView input,
        Action<decimal> setter,
        Action<ValidationException>? onError = null)
    {
        return new InputViewValidatedFormEntry<decimal>(input, Domain.Validator.RequireDecimal, setter, onError);
    }

    public static IValidatedFormEntry Int(
        InputView input,
        Action<int> setter,
        Action<ValidationException>? onError = null)
    {
        return new InputViewValidatedFormEntry<int>(input, Domain.Validator.RequireInt, setter, onError);
    }

    public static IValidatedFormEntry Picker(
        Picker input,
        Action<int> setter,
        Action<ValidationException>? onError = null)
    {
        return new PickerValidatedFormEntry(input, (value) => Validator.RequireGreaterOrEqualsThan(value, 0), setter,
            onError);
    }

    public static IValidatedFormEntry Runnable(Action action,
        Action<ValidationException>? onError = null)
    {
        return new RunnableValidatedFormEntry(action, onError);
    }
}