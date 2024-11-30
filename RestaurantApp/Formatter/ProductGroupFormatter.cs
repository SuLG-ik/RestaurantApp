using RestaurantApp.Model;

namespace RestaurantApp.Formatter;

public class ProductGroupFormatter : BaseFormatter<ProductGroup>
{
    protected override string Format(ProductGroup value)
    {
        return value switch
        {
            ProductGroup.Appetisers => "Закуски",
            ProductGroup.FirstCourses => "Первые блюда",
            ProductGroup.SecondCourses => "Вторые блюда",
            ProductGroup.Dessert => "Десерты",
            ProductGroup.Drinks => "Напитки",
            ProductGroup.Pastries => "Выпечка",
            ProductGroup.Other => "Другое",
            _ => "Неизвестная категория"
        };
    }
}
