using System.Text;
using RestaurantApp.Model;

namespace RestaurantApp.Formatter;

public class SaleFormatter(IFormatter parent) : BaseFormatter<Sale>
{
    protected override string Format(Sale value)
    {
        return new StringBuilder()
            .Append("Продажа: ")
            .Append("дата продажи: ").Append(parent.Format(value.Date))
            .Append("ID ресторана: ").Append(parent.Format(value.RestaurantId))
            .Append("элементы: ").Append(parent.Format(value.SaleItems))
            .ToString();
    }
}