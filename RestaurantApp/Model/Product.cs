namespace RestaurantApp.Model;

public class Product
{
    public string Name { get; }
    public Unit Unit { get; }
    public decimal Price { get; }
    public int Quantity { get; }
    public int SupplierId { get; }

    private Product(string name, Unit unit, decimal price, int quantity, int supplierId)
    {
        Name = name;
        Unit = unit;
        Price = price;
        Quantity = quantity;
        SupplierId = supplierId;
    }

    public class Builder
    {
        private string? _name;
        private Unit? _unit;
        private decimal? _price;
        private int? _quantity;
        private int? _supplierId;

        public Builder SetName(string name)
        {
            _name = Validator.RequireNotBlank(name, nameof(name));
            return this;
        }

        public Builder SetUnit(Unit unit)
        {
            _unit = unit;
            return this;
        }

        public Builder SetPrice(decimal price)
        {
            _price = Validator.RequireGreaterThan(price, 0, nameof(price));
            return this;
        }

        public Builder SetQuantity(int quantity)
        {
            _quantity = Validator.RequireGreaterOrEqualsThan(quantity, 0, nameof(quantity));
            return this;
        }

        public Builder SetSupplierId(int supplierId)
        {
            _supplierId = Validator.RequireGreaterThan(supplierId, 0, nameof(supplierId));
            return this;
        }

        public Product Build()
        {
            var name = Validator.RequireNotNull(_name, nameof(_name));
            var unit = Validator.RequireNotNull(_unit, nameof(_unit));
            var quantity = Validator.RequireNotNull(_quantity, nameof(_quantity));
            var price = Validator.RequireNotNull(_price, nameof(_price));
            var supplier = Validator.RequireNotNull(_supplierId, nameof(_supplierId));
            return new Product(name, unit, price, quantity, supplier);
        }
    }
}