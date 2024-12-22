using RestaurantAppUI.Domain;

namespace RestaurantAppUI.Presentation.Utils;

public class RunnableValidatedFormEntry(
    Action validate,
    Action<ValidationException>? onError = null) : IValidatedFormEntry
{
    public bool Validate()
    {
        try
        {
            validate();
            return true;
        }
        catch (ValidationException e)
        {
            onError?.Invoke(e);
            return false;
        }
    }
}