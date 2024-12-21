using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace RestaurantAppUI.Domain.Model;

public class Product: INameable
{
    public string Name { get; }
    public Unit Unit { get; }
    public ImmutableList<PriceChange> PriceChanges { get; }
    public decimal Quantity { get; }
    public int SupplierId { get; }

    [JsonIgnore] public decimal Price => PriceChanges.Last().Value;

    [JsonConstructor]
    private Product(string name, Unit unit, ImmutableList<PriceChange> priceChanges, decimal quantity, int supplierId)
    {
        Name = name;
        Unit = unit;
        PriceChanges = priceChanges;
        Quantity = quantity;
        SupplierId = supplierId;
    }

    public class Builder
    {
        public Builder()
        {
        }

        public Builder(Product product)
        {
            _name = product.Name;
            _unit = product.Unit;
            _priceChanges = product.PriceChanges.ToList();
            _quantity = product.Quantity;
            _supplierId = product.SupplierId;
        }

        private string? _name;
        private Unit? _unit;
        private List<PriceChange> _priceChanges = [];
        private decimal? _quantity;
        private int? _supplierId;

        public Builder SetName(string name)
        {
            _name = Validator.RequireNotBlank(name);
            return this;
        }

        public Builder SetUnit(Unit unit)
        {
            _unit = unit;
            return this;
        }

        public Builder SetPrice(decimal price)
        {
            _priceChanges.Add(new PriceChange(price, DateTime.Now));
            return this;
        }

        public Builder AddPriceChange(PriceChange priceChange)
        {
            _priceChanges.Add(priceChange);
            return this;
        }

        public Builder AddPriceChanges(IEnumerable<PriceChange> priceChange)
        {
            _priceChanges.AddRange(priceChange);
            return this;
        }

        public Builder SetQuantity(decimal quantity)
        {
            _quantity = Validator.RequireGreaterOrEqualsThan(quantity, 0);
            return this;
        }

        public Builder SetSupplierId(int supplierId)
        {
            _supplierId = Validator.RequireGreaterThan(supplierId, 0);
            return this;
        }

        public Product Build()
        {
            var name = Validator.RequireNotNull(_name);
            var unit = Validator.RequireNotNull(_unit);
            var quantity = Validator.RequireNotNull(_quantity);
            var supplier = Validator.RequireNotNull(_supplierId);
            var priceChanges = Validator.RequireNotEmpty(_priceChanges)
                .OrderBy(p => p.Date);
            return new Product(name, unit, priceChanges.ToImmutableList(), quantity, supplier);
        }
    }
}