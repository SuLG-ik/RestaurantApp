using ConsoleApp1;

namespace RestaurantApp;

public class SystemConsole : IConsole
{
    public void WriteLine(object value)
    {
        Console.WriteLine(value);
    }

    public void Write(object value)
    {
        Console.Write(value);
    }

    public int ReadInt(string tag)
    {
        var line = Validator.RequireNotNull(Console.ReadLine(), tag);
        return Validator.RequireInt(line, tag);
    }

    public int ReadIntUntilValid(string tag, Action? onRetry)
    {
        return Validator.RunUntilValid(() => ReadInt(tag), onRetry);
    }

    public T ReadEnum<T>(string? tag) where T : struct, Enum
    {
        var valueTag = tag ?? typeof(T).Name;
        var value = ReadInt(valueTag);
        return Validator.RequireEnum<T>(value, valueTag);
    }

    public T ReadEnumUntilValid<T>(string? tag = null, Action? onRetry = null) where T : struct, Enum
    {
        return Validator.RunUntilValid(() => ReadEnum<T>(tag), onRetry);
    }

    public string ReadString(string tag)
    {
        return Validator.RequireNotNull(Console.ReadLine(), tag);
    }

    public string ReadStringUntilValid(string tag, Action? onRetry)
    {
        return Validator.RunUntilValid(() => ReadString(tag), onRetry);
    }
}