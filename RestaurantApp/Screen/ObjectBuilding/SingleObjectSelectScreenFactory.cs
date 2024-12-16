using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectSelect;

namespace RestaurantApp.Screen.ObjectBuilding;

public class SingleObjectSelectScreenFactory<T>(
    string title,
    Func<IEnumerable<T>> objectsProvider,
    Action<T> onComplete,
    Action onFailed
) : IScreenFactory where T : class
{
    public Screen CreateScreen()
    {
        return new SingleObjectSelectScreen<T>(title, objectsProvider, onComplete, onFailed);
    }
}