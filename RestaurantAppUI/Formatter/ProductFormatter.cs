using System.Text;
using RestaurantAppUI.Model;

namespace RestaurantAppUI.Formatter;

public class ProductFormatter(IFormatter formatter): BaseFormatter<Product>
{
    protected override string Format(Product value)
    {
        return new StringBuilder().Append("Продукт: наименование: ")
            .Append(value.Name)
            .Append(", стоимость ")
            .Append(value.Price)
            .Append(", количество: ")
            .Append(value.Quantity)
            .Append(", единица измерения: ")
            .Append(formatter.Format(value.Unit))
            .Append(", ID поставщика ")
            .Append(value.SupplierId)
            .ToString();
    }
}