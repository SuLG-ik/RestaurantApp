using System.Collections.Immutable;

namespace RestaurantApp.Model;

public class ProductRequest
{
    public int RestaurantId { get; }
    public DateTime RequestDate { get; }
    public ImmutableList<ProductRequestItem> ProductRequestItems { get; }

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
        private readonly List<ProductRequestItem> _productRequestItems = [];

        public Builder SetRestaurantId(int restaurant)
        {
            _restaurantId = Validator.RequireNotNull(restaurant, nameof(restaurant));
            return this;
        }

        public Builder SetRequestDate(DateTime requestDate)
        {
            _requestDate = requestDate;
            return this;
        }

        public Builder AddProductRequestItem(ProductRequestItem item)
        {
            _productRequestItems.Add(Validator.RequireNotNull(item, nameof(item)));
            return this;
        }

        public ProductRequest Build()
        {
            var restaurantId = Validator.RequireNotNull(_restaurantId, nameof(_restaurantId));
            var requestDate = Validator.RequireNotNull(_requestDate, nameof(_requestDate));
            var productRequestItems = Validator.RequireNotEmpty(_productRequestItems, nameof(_productRequestItems));

            return new ProductRequest(restaurantId, requestDate, productRequestItems.ToImmutableList());
        }
    }
}