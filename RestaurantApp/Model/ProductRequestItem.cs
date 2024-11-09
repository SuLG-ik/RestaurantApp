namespace RestaurantApp.Model;

public class ProductRequestItem
{
    public string ProductName { get; }
    public string Unit { get; }
    public int Quantity { get; }

    private ProductRequestItem(string productName, string unit, int quantity)
    {
        ProductName = productName;
        Unit = unit;
        Quantity = quantity;
    }
    
    public class Builder
    {
        private string? _productName;
        private string? _unit;
        private int? _quantity;

        public Builder SetProductName(string productName)
        {
            _productName = Validator.RequireNotBlank(productName, nameof(productName));
            return this;
        }

        public Builder SetUnit(string unit)
        {
            _unit = Validator.RequireNotBlank(unit, nameof(unit));
            return this;
        }

        public Builder SetQuantity(int quantity)
        {
            _quantity = Validator.RequireGreaterThan(quantity, 0, nameof(quantity)); 
            return this;
        }

        public ProductRequestItem Build()
        {
            var productName = Validator.RequireNotNull(_productName, nameof(_productName));
            var unit = Validator.RequireNotNull(_unit, nameof(_unit));
            var quantity = Validator.RequireNotNull(_quantity, nameof(_quantity));
            return new ProductRequestItem(productName, unit, quantity);
        }
    }
}