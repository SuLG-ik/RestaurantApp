using RestaurantApp.Screen.RegistrationMenuItem;
using RestaurantApp.Screen.RegistrationProductAction;
using RestaurantApp.Screen.RegistrationRestaurant;
using RestaurantApp.Screen.RegistrationSupplierAction;

namespace RestaurantApp.Screen.RegistrationActions;

public class RegistrationActionsScreen : MenuOptionsScreen<RegistrationActionsOptions>
{
    public override string? HeaderMessage => "Регистрационные действия";
    public override Dictionary<RegistrationActionsOptions, MenuOption> Options { get; }

    public RegistrationActionsScreen()
    {
        Options = new Dictionary<RegistrationActionsOptions, MenuOption>
        {
            { RegistrationActionsOptions.NewProduct, new MenuOption("Добавить продукт", OnProduct) },
            { RegistrationActionsOptions.NewSupplier, new MenuOption("Добавить поставщика", OnSupplier) },
            { RegistrationActionsOptions.NewRestaurant, new MenuOption("Добавить ресторан", OnRestaurant) },
            { RegistrationActionsOptions.NewMenuItem, new MenuOption("Добавить пункт меню", OnMenuItem) },
            { RegistrationActionsOptions.Back, new MenuOption("Назад", OnBack) }
        };
    }

    private void OnSupplier()
    {
        Navigator?.NavigateTo(new RegistrationSupplierActionScreen());
    }

    private void OnMenuItem()
    {
        Navigator?.NavigateTo(new RegistrationMenuItemBuilderScreen());
    }

    private void OnRestaurant()
    {
        Navigator?.NavigateTo(new RegistrationRestaurantActionScreen());
    }

    private void OnProduct()
    {
        Navigator?.NavigateTo(new RegistrationProductActionScreen());
    }

    private void OnBack()
    {
        Navigator?.Back();
    }
}