using System.Collections;
using System.Text.RegularExpressions;

namespace RestaurantApp;

public static partial class Validator
{
    public static T RequireGreaterThan<T>(T value, T min, string tag) where T : struct, IComparable<T>
    {
        if (value.CompareTo(min) <= 0)
            throw new ValidationNotCourseInException<T>(value, min, null,
                tag, $"{tag}: Value ({value}) must be greater than {min}");
        return value;
    }

    public static T RequireCourseIn<T>(T value, T min, T max, string tag) where T : struct, IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
        {
            throw new ValidationNotCourseInException<T>(value, min, max, tag,
                $"{tag}: Value ({value}) must be greater than {min} and less than {max}");
        }

        return value;
    }

    public static T RequireGreaterOrEqualsThan<T>(T value, T min, string tag) where T : struct, IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            throw new ValidationNotCourseInException<T>(value, min, null,
                tag, $"{tag}: Value ({value}) must be greater or equals than {min}");
        return value;
    }

    public static T RequireLessOrEqualsThan<T>(T value, T max, string tag) where T : struct, IComparable<T>
    {
        if (value.CompareTo(max) > 0)
        {
            throw new ValidationNotCourseInException<T>(value, null, max,
                tag, $"{tag}: Value ({value}) must be less or equals than {max}");
        }

        return value;
    }

    public static T RequireLessThan<T>(T value, T max, string tag) where T : struct, IComparable<T>
    {
        if (value.CompareTo(max) >= 0)
        {
            throw new ValidationNotCourseInException<T>(value, null, max,
                tag, $"{tag}: Value ({value}) must be less than {max}");
        }

        return value;
    }

    public static T RequireEquals<T>(T value, T requireValue, string tag) where T : struct, IComparable<T>
    {
        if (value.CompareTo(requireValue) != 0)
            throw new ValidationEqualsException<T>(value, requireValue, tag,
                $"{tag}: Value ({value}) must be equals to {requireValue}");
        return value;
    }

    public static string RequireNotEmpty(string value, string tag)
    {
        if (value == "")
            throw new ValidationLengthException<string>(value, 1, int.MaxValue, tag, $"{tag}: Value must be not empty");
        return value;
    }

    public static T RequireNotEmpty<T>(T value, string tag) where T : IEnumerable<object>
    {
        if (!value.Any())
            throw new ValidationLengthException<T>(value, 1, int.MaxValue, tag,
                $"{tag}: Value must be not empty");
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

    public static decimal RequireDecimal(string value, string tag)
    {
        if (!decimal.TryParse(value, out var result))
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