namespace RestaurantApp.Model;

public class Product
{
    public string Name { get; }
    public string Unit { get; }
    public decimal Price { get; }
    public int Quantity { get; }
    public Supplier Supplier { get; }

    private Product(string name, string unit, decimal price, int quantity, Supplier supplier)
    {
        Name = name;
        Unit = unit;
        Price = price;
        Quantity = quantity;
        Supplier = supplier;
    }

    public class Builder
    {
        private string? _name;
        private string? _unit;
        private decimal? _price;
        private int? _quantity;
        private Supplier? _supplier;

        public Builder SetName(string name)
        {
            _name = Validator.RequireNotBlank(name, nameof(name));
            return this;
        }

        public Builder SetUnit(string unit)
        {
            _unit = Validator.RequireNotBlank(unit, nameof(unit));
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

        public Builder SetSupplier(Supplier supplier)
        {
            _supplier = Validator.RequireNotNull(supplier, nameof(supplier));
            return this;
        }

        public Product Build()
        {
            var name = Validator.RequireNotNull(_name, nameof(_name));
            var unit = Validator.RequireNotNull(_unit, nameof(_unit));
            var quantity = Validator.RequireNotNull(_quantity, nameof(_quantity));
            var price = Validator.RequireNotNull(_price, nameof(_price));
            var supplier = Validator.RequireNotNull(_supplier, nameof(_supplier));
            return new Product(name, unit, price, quantity, supplier);
        }
    }
}