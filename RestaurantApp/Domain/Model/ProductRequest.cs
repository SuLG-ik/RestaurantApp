using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace RestaurantApp.Domain.Model;

public class ProductRequest
{
    public int RestaurantId { get; }
    public DateTime RequestDate { get; }
    public ImmutableList<ProductRequestItem> ProductRequestItems { get; }

    [JsonConstructor]
    private ProductRequest(int restaurantId, DateTime requestDate,
        ImmutableList<ProductRequestItem> productRequestItems)
    {
        RestaurantId = restaurantId;
        RequestDate = requestDate;
        ProductRequestItems = productRequestItems;
    }

    public class Builder
    {
        private int? _restaurantId;
        private DateTime? _requestDate;
        private List<ProductRequestItem> _productRequestItems = [];

        public Builder SetRestaurantId(int restaurant)
        {
            _restaurantId = Validator.RequireNotNull(restaurant);
            return this;
        }

        public Builder SetRequestDate(DateTime requestDate)
        {
            _requestDate = requestDate;
            return this;
        }

        public Builder AddProductRequestItem(ProductRequestItem item)
        {
            _productRequestItems.Add(Validator.RequireNotNull(item));
            return this;
        }

        public Builder SetProductRequestItems(IEnumerable<ProductRequestItem> items)
        {
            _productRequestItems = Validator.RequireNotEmpty(items).ToList();
            return this;
        }

        public Builder AddProductRequestItems(IEnumerable<ProductRequestItem> item)
        {
            _productRequestItems.AddRange(Validator.RequireNotNull(item));
            return this;
        }

        public ProductRequest Build()
        {
            var restaurantId = Validator.RequireNotNull(_restaurantId);
            var requestDate = Validator.RequireNotNull(_requestDate);
            var productRequestItems = Validator.RequireNotEmpty(_productRequestItems);

            return new ProductRequest(restaurantId, requestDate, productRequestItems.ToImmutableList());
        }
    }
}