using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public class MenuItem
{
    public string Name { get; }
    public MenuItemGroup Group { get; }
    public ImmutableList<PriceChange> PriceChanges { get; }
    [JsonIgnore] public decimal Price => PriceChanges.Last().Value;


    [JsonConstructor]
    private MenuItem(string name, MenuItemGroup group, ImmutableList<PriceChange> priceChanges)
    {
        Name = name;
        Group = group;
        PriceChanges = priceChanges;
    }

    public class Builder
    {
        private string? _name;
        private MenuItemGroup? _group;
        private List<PriceChange> _priceChanges = [];

        public Builder()
        {
        }

        public Builder(MenuItem menuItem)
        {
            _name = menuItem.Name;
            _group = menuItem.Group;
            _priceChanges = menuItem.PriceChanges.ToList();
        }

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

        public MenuItem Build()
        {
            var name = Validator.RequireNotNull(_name, nameof(_name));
            var group = Validator.RequireNotNull(_group, nameof(_group));
            var priceChanges = Validator.RequireNotEmpty(_priceChanges, nameof(_priceChanges))
                .OrderBy(p => p.Date);

            return new MenuItem(name, group, priceChanges.ToImmutableList());
        }
    }
}