using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public class SaleItem
{
    public int MenuItemId { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    [JsonIgnore] public decimal TotalPrice => Price * Quantity;

    [JsonConstructor]
    private SaleItem(int menuItemId, int quantity, decimal price)
    {
        MenuItemId = menuItemId;
        Quantity = quantity;
        Price = price;
    }

    public class Builder
    {
        private int? _menuItemId;
        private int? _quantity;
        private decimal? _price;

        public Builder()
        {
        }

        public Builder(SaleItem saleItem)
        {
            _menuItemId = saleItem.MenuItemId;
            _quantity = saleItem.Quantity;
            _price = saleItem.Price;
        }

        public Builder SetMenuItemId(int menuItemId)
        {
            _menuItemId = Validator.RequireGreaterThan(menuItemId, 0, nameof(menuItemId));
            return this;
        }

        public Builder SetQuantity(int quantity)
        {
            _quantity = Validator.RequireGreaterOrEqualsThan(quantity, 0, nameof(quantity));
            return this;
        }

        public Builder SetPrice(decimal price)
        {
            _price = Validator.RequireGreaterOrEqualsThan(price, 0, nameof(price));
            return this;
        }

        public SaleItem Build()
        {
            var menuItemId = Validator.RequireNotNull(_menuItemId, nameof(_menuItemId));
            var quantity = Validator.RequireNotNull(_quantity, nameof(_quantity));
            var price = Validator.RequireNotNull(_price, nameof(_price));

            return new SaleItem(menuItemId, quantity, price);
        }
    }
}