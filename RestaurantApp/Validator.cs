using System.Text.RegularExpressions;
using ConsoleApp1;

namespace RestaurantApp;

public partial class Validator
{
    public static int RequireGreaterThan(int value, int min, string tag)
    {
        if (value <= min)
            throw new ValidationNotCourseInException<int>(value, min, int.MaxValue,
                tag, $"{tag}: Value ({value}) must be greater than {min}");
        return value;
    }
    public static decimal RequireGreaterThan(decimal value, decimal min, string tag)
    {
        if (value <= min)
            throw new ValidationNotCourseInException<decimal>(value, min, decimal.MaxValue,
                tag, $"{tag}: Value ({value}) must be greater than {min}");
        return value;
    }

    public static int RequireCourseIn(int value, int min, int max, string tag)
    {
        if (value < min || value > max)
            throw new ValidationNotCourseInException<int>(value, min, max, tag,
                $"{tag}: Value ({value}) must be greater than {min} and less than {max}");
        return value;
    }

    public static int RequireGreaterOrEqualsThan(int value, int min, string tag)
    {
        return RequireCourseIn(value, min, int.MaxValue, tag);
    }

    public static int RequireLessOrEqualsThan(int value, int max, string tag)
    {
        return RequireCourseIn(value, int.MinValue, max, tag);
    }

    public static int RequireLessThan(int value, int max, string tag)
    {
        if (value >= max)
            throw new ValidationNotCourseInException<int>(value, int.MinValue, max,
                tag, $"{tag}: Value ({value}) must be less than {max}");
        return value;
    }

    public static int RequireEquals(int value, int requireValue, string tag)
    {
        if (value != requireValue)
            throw new ValidationEqualsException<int>(value, requireValue, tag,
                $"{tag}: Value ({value}) must be equals to {requireValue}");
        return value;
    }

    public static string RequireNotEmpty(string value, string tag)
    {
        if (value == "")
            throw new ValidationLengthException<string>(value, 1, int.MaxValue, tag, $"{tag}: Value must be not empty");
        return value;
    }
    
    public static List<T> RequireNotEmpty<T>(List<T> value, string tag)
    {
        if (value.Count == 0)
            throw new ValidationLengthException<List<T>>(value, 1, int.MaxValue, tag, $"{tag}: Value must be not empty");
        return value;
    }

    public static string RequireNotBlank(string value, string tag)
    {
        if (value.Trim() == "")
            throw new ValidationNotBlankException(value, tag, $"{tag}: Value ({value}) must be not blank");
        return value;
    }

    public static T RequireNotNull<T>(T? value, string tag)
    {
        if (value == null) throw new ValidationNullException(tag, $"{tag}: Value must be not null");
        return value;
    }

    public static T RequireNotNull<T>(T? value, string tag) where T : struct
    {
        if (value == null) throw new ValidationNullException(tag, $"{tag}: Value must be not null");
        return value.Value;
    }

    public static int RequireInt(string value, string tag)
    {
        if (!int.TryParse(value, out var result))
            throw new ValidationConvertException<string>(value, typeof(int), tag,
                $"{tag}: Value ({value}) must be int");
        return result;
    }


    public static T RequireEnum<T>(int value, string tag) where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value))
            throw new ValidationConvertException<int>(value, typeof(T), tag,
                $"{tag}: Value ({value}) must be defined in {typeof(T)}");
        return (T)Enum.ToObject(typeof(T), value);
    }

    public static int RequireLong(string value, string tag)
    {
        if (!long.TryParse(value, out var result))
            throw new ValidationConvertException<string>(value, typeof(long), tag,
                $"{tag}: Value ({value}) must be int");
        return (int)result;
    }

    public static bool RequireBool(string value, string tag)
    {
        if (!bool.TryParse(value, out var result))
            throw new ValidationConvertException<string>(value, typeof(bool), tag,
                $"{tag}: Value ({value}) must be boolean");
        return result;
    }
    
    public static string RequireNumeric(string value, string tag)
    {
        var isNumber = NumericRegex().IsMatch(value);
        if (!isNumber)
            throw new ValidationCheckException<string>(value, tag, $"Value ({value}) must be numeric");
        return value;
    }


    public static T RunUntilValid<T>(Func<T> func, Action? onRetry = null)
    {
        while (true)
        {
            try
            {
                return func();
            }
            catch (ValidationNullException exception)
            {
                throw new ConsoleNotAvailableException(exception);
            }
            catch (ValidationException)
            {
                onRetry?.Invoke();
            }
        }
    }

    public static void RunUntilValid(Action func, Action? onRetry = null)
    {
        while (true)
        {
            try
            {
                func();
                break;
            }
            catch (ValidationNullException exception)
            {
                throw new ConsoleNotAvailableException(exception);
            }
            catch (ValidationException)
            {
                onRetry?.Invoke();
            }
        }
    }

    [GeneratedRegex(@"^\d+$")]
    private static partial Regex NumericRegex();
}