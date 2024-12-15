namespace RestaurantApp;

public class ValidationException(string tag, string message) : ArgumentException
{
    public string Tag { get; } = tag;
    public override string Message { get; } = message;
}

public class ValidationConvertException<T>(T actual, Type expectedType, string tag, string message)
    : ValidationException(tag, message)
{
    public T Actual { get; } = actual;
    public Type ExpectedType { get; } = expectedType;
}

public class ValidationCheckException<T>(T actual, string tag, string message)
    : ValidationException(tag, message)
{
    public T Actual { get; } = actual;
}

public class ValidationLengthException<T>(T actual, int minLength, int maxLength, string tag, string message)
    : ValidationException(tag, message)
{
    public T Actual { get; } = actual;
    public int MinLength { get; } = minLength;
    public int MaxLength { get; } = maxLength;
}

public class ValidationNotBlankException(string actual, string tag, string message) : ValidationException(tag, message)
{
    public string Actual { get; } = actual;
}

public class ValidationNotCourseInException<T>(T actual, T? minValue, T? maxValue, string tag, string message)
    : ValidationException(tag, message) where T : struct
{
    public T Actual { get; } = actual;
    public T? MinValue { get; } = minValue;
    public T? MaxValue { get; } = maxValue;
}

public class ValidationEqualsException<T>(T actual, T expected, string tag, string message)
    : ValidationException(tag, message)
{
    public T Actual { get; } = actual;
    public T Expected { get; } = expected;
}

public class ValidationNullException(string tag, string message) : ValidationException(tag, message);