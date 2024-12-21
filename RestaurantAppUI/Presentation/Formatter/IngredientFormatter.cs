using System.Text;
using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Presentation.Formatter;

public class IngredientFormatter(IFormatter formatter) : BaseFormatter<Ingredient>
{
    private IProductRepository? _productRepository;

    private IProductRepository ProductRepository
    {
        get { return _productRepository ??= ServiceLocator.GetService<IProductRepository>(); }
    }

    protected override string Format(Ingredient value)
    {
        var product = Domain.Validator.RequireNotNull(ProductRepository.Find(value.ProductId));
        return new StringBuilder()
            .Append("Продукт: ").Append(product.Data.Name)
            .Append(", количество: ").Append(value.Quantity)
            .Append(", единица измерения: ").Append(formatter.Format(product.Data.Unit))
            .ToString();
    }
}