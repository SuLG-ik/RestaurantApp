namespace RestaurantApp;

public class StackNavigator<T> : INavigator<T>
{
    public StackNavigator(T? initialScreen)
    {
        ArgumentNullException.ThrowIfNull(initialScreen);
        _backstack = [initialScreen];
    }

    public StackNavigator()
    {
        _backstack = [];
    }

    private readonly List<T> _backstack;

    public T? CurrentScreen => _backstack.LastOrDefault();

    public bool Contains(T screen)
    {
        return _backstack.Contains(screen);
    }

    public void NavigateTo(T screen)
    {
        _backstack.Add(screen);
    }

    public bool ReplaceCurrent(T screen)
    {
        if (_backstack.Count < 1) return false;
        _backstack[^1] = screen;
        return true;
    }

    public void ReplaceCurrentOrNavigate(T screen)
    {
        if (ReplaceCurrent(screen)) return;
        NavigateTo(screen);
    }

    public bool Back()
    {
        if (_backstack.Count < 1) return false;
        _backstack.RemoveAt(_backstack.Count - 1);
        return true;
    }
}