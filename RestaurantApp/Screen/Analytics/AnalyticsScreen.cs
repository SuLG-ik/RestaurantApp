namespace RestaurantApp.Screen.Analytics;

public class AnalyticsScreen : MenuOptionsScreen<AnalyticsOptions>
{
    public override string? HeaderMessage => "Аналитика";

    public override Dictionary<AnalyticsOptions, MenuOption> Options => new()
    {
        { AnalyticsOptions.RestaurantProducts, new MenuOption("Контроль продуктов в ресторане", OnRestaurantProducts) },
        { AnalyticsOptions.SalesRevenue, new MenuOption("Выручка ресторана", OnRestaurantSalesRevenue) },
        { AnalyticsOptions.Back, new MenuOption("Назад", OnBack) },
    };

    private void OnRestaurantProducts()
    {
        Navigator?.NavigateTo(new RestaurantProductsScreen());
    }

    private void OnRestaurantSalesRevenue()
    {
        Navigator?.NavigateTo(new RestaurantSalesRevenueScreen());
    }

    private void OnBack()
    {
        Navigator?.Back();
    }
}