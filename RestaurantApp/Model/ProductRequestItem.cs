namespace RestaurantApp.Model;

public class ProductRequestItem
{
    public int ProductId { get; }
    public Unit Unit { get; }
    public int Quantity { get; }
    public decimal PurchasePrice { get; }
    
    private ProductRequestItem(int productId, Unit unit, int quantity, decimal purchasePrice)
    {
        ProductId = productId;
        Unit = unit;
        Quantity = quantity;
        PurchasePrice = purchasePrice;
    }

    public class Builder
    {
        private int? _productId;
        private Unit? _unit;
        private int? _quantity;
        private decimal? _purchasePrice;

        public Builder SetProductId(int productId)
        {
            _productId = Validator.RequireGreaterThan(productId, 0, nameof(productId));
            return this;
        }

        public Builder SetUnit(Unit unit)
        {
            _unit = unit;
            return this;
        }

        public Builder SetQuantity(int quantity)
        {
            _quantity = Validator.RequireGreaterThan(quantity, 0, nameof(quantity));
            return this;
        }

        public Builder SetPurchasePrice(decimal purchasePrice)
        {
            _purchasePrice = Validator.RequireGreaterThan(purchasePrice, 0, nameof(purchasePrice));
            return this;
        }

        public ProductRequestItem Build()
        {
            var productId = Validator.RequireNotNull(_productId, nameof(_productId));
            var unit = Validator.RequireNotNull(_unit, nameof(_unit));
            var quantity = Validator.RequireNotNull(_quantity, nameof(_quantity));
            var purchasePrice = Validator.RequireNotNull(_purchasePrice, nameof(_purchasePrice));
            return new ProductRequestItem(productId, unit, quantity, purchasePrice);
        }
    }
}