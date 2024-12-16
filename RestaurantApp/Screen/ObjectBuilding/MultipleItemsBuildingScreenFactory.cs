namespace RestaurantApp.Screen.ObjectBuilding;

public class MultipleItemsBuildingScreenFactory<T>(
    string title,
    IParametrizedScreenFactory<MultipleItemsParams<T>> factory,
    Action<List<T>> onComplete,
    bool required = false
) : IScreenFactory where T : class
{
    public Screen CreateScreen()
    {
        return new MultipleItemsBuildingScreen<T>(title, factory, onComplete, required);
    }
}