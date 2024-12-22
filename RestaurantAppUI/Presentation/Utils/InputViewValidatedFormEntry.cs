using RestaurantAppUI.Domain;

namespace RestaurantAppUI.Presentation.Utils;

public class InputViewValidatedFormEntry<T>(
    InputView input,
    Func<string, T> validator,
    Action<T> setter,
    Action<ValidationException>? onError) : IValidatedFormEntry
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