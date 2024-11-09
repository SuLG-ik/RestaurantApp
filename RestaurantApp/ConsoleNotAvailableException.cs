namespace RestaurantApp;

public class ConsoleNotAvailableException(Exception? innerException = null)
    : InvalidOperationException(message: "Console is not available", innerException: innerException);