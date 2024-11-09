namespace RestaurantApp;

public interface INavigator<T>
{
    public T? CurrentScreen { get; }
    public void NavigateTo(T screen);
    public bool ReplaceCurrent(T screen);
    public void ReplaceCurrentOrNavigate(T screen);
    public bool Back();
}