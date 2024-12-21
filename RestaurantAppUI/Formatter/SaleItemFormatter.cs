using System.Text;
using RestaurantAppUI.Model;
using RestaurantAppUI.Repository;

namespace RestaurantAppUI.Formatter;

public class SaleItemFormatter : BaseFormatter<SaleItem>
{
    private IMenuItemRepository? _menuItemRepository;

    private IMenuItemRepository MenuItemRepository =>
        _menuItemRepository ??= ServiceLocator.GetService<IMenuItemRepository>();

    protected override string Format(SaleItem value)
    {
        var menuItem = Validator.RequireNotNull(MenuItemRepository.Find(value.MenuItemId));
        return new StringBuilder()
            .Append($"Пункт меню: {menuItem.Data.Name} (ID: {menuItem.Id})")
            .Append(", количество: ")
            .Append(value.Quantity)
            .Append(", стоимость: ")
            .Append(value.TotalPrice)
            .ToString();
    }
}