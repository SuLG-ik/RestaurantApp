using System.Collections.Immutable;

namespace RestaurantApp.Model;

public class ProductRequest
{
    public Restaurant Restaurant { get; }
    public DateTime RequestDate { get; }
    public ImmutableList<ProductRequestItem> ProductRequestItems { get; }

    private ProductRequest(Restaurant restaurant, DateTime requestDate,
        ImmutableList<ProductRequestItem> productRequestItems)
    {
        Restaurant = restaurant;
        RequestDate = requestDate;
        ProductRequestItems = productRequestItems;
    }

    public class Builder
    {
        private Restaurant? _restaurant;
        private DateTime? _requestDate;
        private readonly List<ProductRequestItem> _productRequestItems = [];

        public Builder SetRestaurant(Restaurant restaurant)
        {
            _restaurant = Validator.RequireNotNull(restaurant, nameof(restaurant));
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
            var restaurant = Validator.RequireNotNull(_restaurant, nameof(_restaurant));
            var requestDate = Validator.RequireNotNull(_requestDate, nameof(_requestDate));
            var productRequestItems = Validator.RequireNotEmpty(_productRequestItems, nameof(_productRequestItems));

            return new ProductRequest(restaurant, requestDate, productRequestItems.ToImmutableList());
        }
    }
}