using RestaurantApp.Model;
using RestaurantApp.Repository;
using RestaurantApp.Screen.ObjectSelect;

namespace RestaurantApp.Screen.ObjectBuilding;

public class MultipleObjectsSelectScreenFactory<T>(
    string title,
    IRepository<T> repository,
    Action<List<SavedModel<T>>> onComplete,
    Action onFailed
) : IScreenFactory where T : class
{
    public Screen CreateScreen()
    {
        return new MultipleObjectSelectScreen<T>(title, repository, onComplete, onFailed);
    }
}