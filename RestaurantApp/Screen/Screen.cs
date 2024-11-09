namespace RestaurantApp.Screen;

public abstract class Screen
{
    protected INavigator<Screen>? Navigator { get; private set; }
    
    public void BaseCreate(INavigator<Screen> navigator)
    {
        Navigator = navigator;
        Create();
    }

    /// <summary>
    /// Calls ones when screen became alive.
    /// </summary>
    protected virtual void Create()
    {
    }
    
    
    /// <summary>
    /// Calls every time when screen is active and alive on each circle tick
    /// </summary>
    public abstract void Display();

    public void BaseDestroy()
    {
        Destroy();
        Navigator = null;
    }

    /// <summary>
    /// Calls ones when screen stop being alive
    /// </summary>
    protected virtual void Destroy()
    {
    }
}