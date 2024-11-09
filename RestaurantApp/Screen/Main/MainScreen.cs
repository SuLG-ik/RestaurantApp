using ConsoleApp1;

namespace RestaurantApp.Screen.Main;

public class MainScreen: Screen
{
    
    private IConsole _console;

    protected override void Create()
    {
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        ShowMenu();
        switch (ReadSelection())
        {
            case MainMenuOptions.Quit:
                Navigator?.Back();
                break;
            case MainMenuOptions.Registration:
                break;
            default:
                ShowRetryMessage();
                break;
        }
    }
    
    private MainMenuOptions ReadSelection()
    {
        _console.Write("Выберите действие: ");
        return _console.ReadEnumUntilValid<MainMenuOptions>(onRetry: ShowRetryMessage);
    }

    private void ShowRetryMessage()
    {
        _console.Write("Такого пункта меню нет! Попробуйте снова: ");
    }
    
    private void ShowMenu()
    {
        _console.WriteLine("Главное меню. Выберите номер операции");
        _console.WriteLine("1. Регистрационные действия");
        _console.WriteLine("0. Выход");
    }
}