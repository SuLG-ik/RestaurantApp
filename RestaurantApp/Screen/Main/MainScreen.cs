using RestaurantApp.Screen.Analytics;
using RestaurantApp.Screen.CreateRequest;
using RestaurantApp.Screen.CreateSale;
using RestaurantApp.Screen.PrintInfo;
using RestaurantApp.Screen.RegistrationActions;

namespace RestaurantApp.Screen.Main;

public class MainScreen : MenuOptionsScreen<MainMenuOptions>
{
    public override string? HeaderMessage => "Главное меню";

    public override Dictionary<MainMenuOptions, MenuOption> Options => new()
    {
        { MainMenuOptions.CreateRequest, new MenuOption("Создать запрос на продукты", OnCreateRequest) },
        { MainMenuOptions.CreateSale, new MenuOption("Создание продажи", OnCreateSale) },
        { MainMenuOptions.Registration, new MenuOption("Регистрационные действия", OnRegistrationActions) },
        { MainMenuOptions.PrintInfo, new MenuOption("Вывод данных", OnPrintInfo) },
        { MainMenuOptions.Analytics, new MenuOption("Аналитика", OnAnalytics) },
        { MainMenuOptions.Quit, new MenuOption("Выход", OnQuit) },
    };

    private void OnRegistrationActions()
    {
        Navigator?.NavigateTo(new RegistrationActionsScreen());
    }


    private void OnAnalytics()
    {
        Navigator?.NavigateTo(new AnalyticsScreen());
    }


    private void OnCreateSale()
    {
        Navigator?.NavigateTo(new CreateSaleScreen());
    }

    private void OnPrintInfo()
    {
        Navigator?.NavigateTo(new PrintItemsScreen());
    }

    private void OnCreateRequest()
    {
        Navigator?.NavigateTo(new CreateRequestScreen());
    }

    private void OnQuit()
    {
        Navigator?.Back();
    }
};