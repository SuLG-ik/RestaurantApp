namespace RestaurantApp.Screen.ObjectBuilding;

public class MultipleItemsParams<T>(
    Action<T> action,
    List<T> items
)
{
    public Action<T> Action => action;
    public List<T> Items => items;
}