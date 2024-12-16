using System.Text;
using RestaurantApp.Model;

namespace RestaurantApp.Formatter;

public class MenuItemFormatter(IFormatter generalFormatter) : BaseFormatter<MenuItem>
{
    protected override string Format(MenuItem value)
    {
        return new StringBuilder().Append("Пункт меню: ")
            .Append("Название: ").Append(value.Name)
            .Append(", группа: ").Append(generalFormatter.Format(value.Group))
            .Append(", цена: ").Append(value.Price)
            .Append(", ингредиенты: ").Append(generalFormatter.Format(value.Ingredients))
            .ToString();
    }
}