using System.Text;
using RestaurantAppUI.Model;

namespace RestaurantAppUI.Formatter;

public class RestaurantFormatter : BaseFormatter<Restaurant>
{
    protected override string Format(Restaurant value)
    {
        return new StringBuilder().Append("Ресторан: ")
            .Append("Наименование: ").Append(value.Name)
            .Append(", адрес: ").Append(value.Address)
            .Append(", директор: ").Append(value.DirectorFullname)
            .Append(", телефон: ").Append(value.PhoneNumber)
            .ToString();
    }
}