using System.Text;
using RestaurantAppUI.Model;

namespace RestaurantAppUI.Formatter;

public class ProductRequestItemFormatter : BaseFormatter<ProductRequestItem>
{
    protected override string Format(ProductRequestItem value)
    {
        return new StringBuilder()
            .Append("ID продукта: ").Append(value.ProductId)
            .Append(", количество: ").Append(value.Quantity)
            .ToString();
    }
}