namespace RestaurantApp.Screen.ObjectBuilding;

public class MultipleItemsBuildingScreenFactory<T>(
    string title,
    IParametrizedScreenFactory<Action<T>> factory,
    Action<List<T>> onComplete
) : IScreenFactory where T : class
{
    public Screen CreateScreen()
    {
        return new MultipleItemsBuildingScreen<T>(title, factory, onComplete);
    }
}