namespace RestaurantAppUI.Domain.Model;

public class ProductEditing(SavedModel<Product> product, decimal quantityEditing)
{
    public SavedModel<Product> Product { get; } = Validator.RequireNotNull(product);

    public decimal QuantityEditing { get; } =
        Validator.RequireGreaterOrEqualsThan(quantityEditing, 0);

    public ProductEditing(SavedModel<Product> product) : this(product, product.Data.Quantity)
    {
    }
}