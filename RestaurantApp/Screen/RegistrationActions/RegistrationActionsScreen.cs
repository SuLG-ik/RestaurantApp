using RestaurantApp.Screen.RegistrationProductAction;
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
            { RegistrationActionsOptions.Back, new MenuOption("Назад", OnBack) }
        };
    }

    private void OnSupplier()
    {
        Navigator?.ReplaceCurrent(new RegistrationSupplierActionScreen());
    }

    private void OnProduct()
    {
        Navigator?.ReplaceCurrent(new RegistrationProductActionScreen());
    }

    private void OnBack()
    {
        Navigator?.Back();
    }
}