using System.Text;
using RestaurantAppUI.Domain;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Presentation.Formatter;

public class ProductRequestItemFormatter(IFormatter formatter) : BaseFormatter<ProductRequestItem>
{
    private Lazy<IProductRepository> _productRepository = new(ServiceLocator.GetService<IProductRepository>);

    protected override string Format(ProductRequestItem value)
    {
        var product = Validator.RequireNotNull(_productRepository.Value.Find(value.ProductId));
        return new StringBuilder()
            .Append("Продукт: ").Append(formatter.Format(product))
            .Append(", количество: ").Append(value.Quantity)
            .ToString();
    }
}