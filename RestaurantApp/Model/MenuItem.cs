using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public class MenuItem
{
    public string Name { get; }
    public MenuItemGroup Group { get; }
    public decimal Price { get; }

    [JsonConstructor]
    private MenuItem(string name, MenuItemGroup group, decimal price)
    {
        Name = name;
        Group = group;
        Price = price;
    }

    public class Builder
    {
        private string? _name;
        private MenuItemGroup? _group;
        private decimal? _price;

        public Builder SetName(string name)
        {
            _name = Validator.RequireNotBlank(name, nameof(name));
            return this;
        }

        public Builder SetGroup(MenuItemGroup group)
        {
            _group = Validator.RequireNotNull(group, nameof(group));
            return this;
        }

        public Builder SetPrice(decimal price)
        {
            _price = Validator.RequireGreaterThan(price, 0, nameof(price));
            return this;
        }

        public MenuItem Build()
        {
            var name = Validator.RequireNotNull(_name, nameof(_name));
            var group = Validator.RequireNotNull(_group, nameof(_group));
            var price = Validator.RequireNotNull(_price, nameof(_price));

            return new MenuItem(name, group, price);
        }
    }
}