namespace RestaurantApp.Screen;

public abstract class Screen
{
    protected INavigator<Screen>? Navigator { get; private set; }
    private bool _isInitialized;

    public void BaseCreate(INavigator<Screen> navigator)
    {
        Navigator = navigator;
        if (!_isInitialized)
        {
            Create();
            Resume();
            _isInitialized = true;
        }
        else
        {
            Resume();
        }
    }

    /// <summary>
    /// Calls ones when screen became alive.
    /// </summary>
    protected virtual void Create()
    {
    }

    /// <summary>
    /// Calls ones when screen shown again.
    /// </summary>
    protected virtual void Resume()
    {
    }


    /// <summary>
    /// Calls multiple times when screen lose context.
    /// </summary>
    protected virtual void Pause()
    {
    }


    /// <summary>
    /// Calls every time when screen is active and alive on each circle tick
    /// </summary>
    public abstract void Display();

    public void BaseDestroy()
    {
        if (Navigator?.Contains(this) == true)
        {
            Pause();
        }
        else
        {
            Pause();
            Destroy();
            Navigator = null;
        }
    }

    /// <summary>
    /// Calls ones when screen stop being alive
    /// </summary>
    protected virtual void Destroy()
    {
    }
}