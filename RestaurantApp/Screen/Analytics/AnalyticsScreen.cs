namespace RestaurantApp.Screen.Analytics;

public class AnalyticsScreen : MenuOptionsScreen<AnalyticsOptions>
{
    public override string? HeaderMessage => "Аналитика";

    public override Dictionary<AnalyticsOptions, MenuOption> Options => new()
    {
        { AnalyticsOptions.RestaurantProducts, new MenuOption("Контроль продуктов в ресторане", OnRestaurantProducts) },
        { AnalyticsOptions.Back, new MenuOption("Назад", OnBack) },
    };

    private void OnRestaurantProducts()
    {
        Navigator?.NavigateTo(new RestaurantProductsScreen());
    }

    private void OnBack()
    {
        Navigator?.Back();
    }
}