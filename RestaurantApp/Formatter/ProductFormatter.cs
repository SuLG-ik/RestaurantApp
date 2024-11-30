using System.Text;
using RestaurantApp.Model;

namespace RestaurantApp.Formatter;

public class ProductFormatter : BaseFormatter<Product>
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
            .Append(value.Unit)
            .Append(", ID поставщика ")
            .Append(value.SupplierId)
            .ToString();
    }
}