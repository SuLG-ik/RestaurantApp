using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public class Sale
{
    public int RestaurantId { get; }
    public DateTime Date { get; }
    public ImmutableList<SaleItem> SaleItems { get; }

    [JsonIgnore]
    public decimal TotalPrice
    {
        get { return SaleItems.Sum((item) => item.Quantity * item.Price); }
    }

    [JsonConstructor]
    private Sale(int restaurantId, DateTime date, ImmutableList<SaleItem> saleItems)
    {
        RestaurantId = restaurantId;
        Date = date;
        SaleItems = saleItems;
    }

    public class Builder
    {
        private int? _restaurantId;
        private DateTime? _date;
        private List<SaleItem> _saleItems = [];

        public Builder SetRestaurantId(int restaurantId)
        {
            _restaurantId = Validator.RequireGreaterThan(restaurantId, 0, nameof(restaurantId));
            return this;
        }

        public Builder SetDate(DateTime date)
        {
            _date = Validator.RequireNotNull(date, nameof(date));
            return this;
        }

        public Builder AddSaleItem(SaleItem saleItems)
        {
            _saleItems.Add(saleItems);
            return this;
        }

        public Builder AddSaleItems(IEnumerable<SaleItem> saleItems)
        {
            _saleItems.AddRange(saleItems);
            return this;
        }

        public Builder SetSaleItems(IEnumerable<SaleItem> saleItems)
        {
            _saleItems = saleItems.ToList();
            return this;
        }

        public Sale Build()
        {
            var restaurantId = Validator.RequireNotNull(_restaurantId, nameof(_restaurantId));
            var date = Validator.RequireNotNull(_date, nameof(_date));
            var saleItems = Validator.RequireNotEmpty(_saleItems, nameof(_saleItems));

            return new Sale(restaurantId, date, saleItems.ToImmutableList());
        }
    }
}