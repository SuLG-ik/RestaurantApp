using RestaurantApp.Screen.RegistrationActions;

namespace RestaurantApp.Screen.Main;

public class MainScreen : MenuOptionsScreen<MainMenuOptions>
{
    public override string? HeaderMessage => "Главное меню";

    public override Dictionary<MainMenuOptions, MenuOption> Options { get; }

    public MainScreen()
    {
        Options = new Dictionary<MainMenuOptions, MenuOption>
        {
            { MainMenuOptions.Registration, new MenuOption("Регистрационные действия", OnRegistrationActions) },
            { MainMenuOptions.Quit, new MenuOption("Выход", OnQuit) },
        };
    }

    private void OnRegistrationActions()
    {
        Navigator?.NavigateTo(new RegistrationActionsScreen());
    }

    private void OnQuit()
    {
        Navigator?.Back();
    }
};