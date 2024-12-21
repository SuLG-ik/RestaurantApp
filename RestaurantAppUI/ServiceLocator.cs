namespace RestaurantAppUI;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Services = new();

    public static T GetService<T>() where T : class
    {
        if (!Contains<T>()) throw new KeyNotFoundException($"No service registered for {typeof(T)}");
        return (T)Services[typeof(T)];
    }

    public static void Register<T>(T service) where T : class
    {
        if (Contains<T>()) throw new InvalidOperationException($"Service already registered for {typeof(T)}");
        Services[typeof(T)] = service;
    }

    public static bool Contains<T>() where T : class
    {
        return Services.ContainsKey(typeof(T));
    }

    public static void Reset()
    {
        Services.Clear();
    }
}