using System.Text;
using RestaurantAppUI.Domain.Model;

namespace RestaurantAppUI.Formatter;

public class ProductEditingFormatter(IFormatter formatter) : BaseFormatter<ProductEditing>
{
    protected override string Format(ProductEditing value)
    {
        var product = value.Product.Data;
        return new StringBuilder().Append(formatter.Format(product))
            .Append(", редактируемое количество: ")
            .Append(value.QuantityEditing)
            .ToString();
    }
}