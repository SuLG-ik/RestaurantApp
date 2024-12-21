using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Formatter;

public class ProductGroupFormatter : BaseFormatter<MenuItemGroup>
{
    protected override string Format(MenuItemGroup value)
    {
        return value switch
        {
            MenuItemGroup.Appetisers => "Закуски",
            MenuItemGroup.FirstCourses => "Первые блюда",
            MenuItemGroup.SecondCourses => "Вторые блюда",
            MenuItemGroup.Dessert => "Десерты",
            MenuItemGroup.Drinks => "Напитки",
            MenuItemGroup.Pastries => "Выпечка",
            MenuItemGroup.Other => "Другое",
            _ => "Неизвестная категория"
        };
    }
}
