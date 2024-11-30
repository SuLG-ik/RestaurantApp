namespace RestaurantApp;

public interface IConsole
{
    void WriteLine(object value);
    
    void Write(object value);
    
    int ReadInt(string tag);

    int ReadIntUntilValid(string tag, Action? onRetry = null);
    
    decimal ReadDecimal(string tag);

    decimal ReadDecimalUntilValid(string tag, Action? onRetry = null);

    T ReadEnum<T>(string? tag = null) where T : struct, Enum;
    
    T ReadEnumUntilValid<T>(string? tag = null, Action? onRetry = null) where T : struct, Enum;

    string ReadString(string tag);
    
    string ReadStringUntilValid(string tag, Action? onRetry = null);
}