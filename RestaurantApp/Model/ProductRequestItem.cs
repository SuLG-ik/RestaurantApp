using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public class ProductRequestItem
{
    public int ProductId { get; }
    public decimal Quantity { get; }
    
    [JsonConstructor]
    private ProductRequestItem(int productId, decimal quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public class Builder
    {
        private int? _productId;
        private decimal? _quantity;

        public Builder SetProductId(int productId)
        {
            _productId = Validator.RequireGreaterThan(productId, 0, nameof(productId));
            return this;
        }

        public Builder SetQuantity(decimal quantity)
        {
            _quantity = Validator.RequireGreaterThan(quantity, 0, nameof(quantity));
            return this;
        }

        public ProductRequestItem Build()
        {
            var productId = Validator.RequireNotNull(_productId, nameof(_productId));
            var quantity = Validator.RequireNotNull(_quantity, nameof(_quantity));
            return new ProductRequestItem(productId, quantity);
        }
    }
}