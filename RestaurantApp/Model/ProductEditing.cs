namespace RestaurantApp.Model;

public class ProductEditing(SavedModel<Product> product, decimal quantityEditing)
{
    public SavedModel<Product> Product { get; } = Validator.RequireNotNull(product, nameof(product));

    public decimal QuantityEditing { get; } =
        Validator.RequireGreaterOrEqualsThan(quantityEditing, 0, nameof(quantityEditing));

    public ProductEditing(SavedModel<Product> product) : this(product, product.Data.Quantity)
    {
    }
}