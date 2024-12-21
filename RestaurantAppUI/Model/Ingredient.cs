using System.Text.Json.Serialization;

namespace RestaurantAppUI.Model;

public class Ingredient
{
    public int ProductId { get; }
    public decimal Quantity { get; }


    [JsonConstructor]
    private Ingredient(int productId, decimal quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public class Builder
    {
        private int _productId;
        private decimal _quantity;

        public Builder SetProductId(int productId)
        {
            _productId = Validator.RequireGreaterThan(productId, 0);
            return this;
        }

        public Builder SetQuantity(decimal quantity)
        {
            _quantity = Validator.RequireGreaterThan(quantity, 0);
            return this;
        }

        public Ingredient Build()
        {
            return new Ingredient(_productId, _quantity);
        }
    }
}