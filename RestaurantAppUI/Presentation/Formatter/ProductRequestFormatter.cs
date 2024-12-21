using System.Text;
using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Presentation.Formatter;

public class ProductRequestFormatter(IFormatter formatter) : BaseFormatter<ProductRequest>
{
    protected override string Format(ProductRequest value)
    {
        return new StringBuilder().Append("Запрос на продукты: ")
            .Append("ID ресторана: ").Append(value.RestaurantId)
            .Append(", дата запроса: ").Append(formatter.Format(value.RequestDate))
            .Append(", продукты: ").Append(formatter.Format(value.ProductRequestItems))
            .ToString();
    }
}