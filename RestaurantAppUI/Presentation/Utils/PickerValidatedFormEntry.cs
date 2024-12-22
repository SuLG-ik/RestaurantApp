using RestaurantApp.Domain;

namespace RestaurantAppUI.Presentation.Utils;

public class PickerValidatedFormEntry(
    Picker picker,
    Func<int, int> validator,
    Action<int> setter,
    Action<ValidationException>? onError) : IValidatedFormEntry
{
    public bool Validate()
    {
        try
        {
            var value = validator.Invoke(picker.SelectedIndex);
            setter.Invoke(value);
            if (onError == null)
            {
                picker.BackgroundColor = Colors.Transparent;
            }

            return true;
        }
        catch (ValidationException ex)
        {
            if (onError == null)
            {
                picker.BackgroundColor = Colors.Red;
            }
            else
            {
                onError(ex);
            }

            return false;
        }
    }
};