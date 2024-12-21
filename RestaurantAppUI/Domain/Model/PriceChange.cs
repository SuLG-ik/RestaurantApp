using System.Text.Json.Serialization;

namespace RestaurantAppUI.Domain.Model;

public class PriceChange
{
    [JsonConstructor]
    public PriceChange(decimal value, DateTime date)
    {
        Value = value;
        Date = date;
    }

    private decimal _value;

    public decimal Value
    {
        get => _value;
        private set { _value = Validator.RequireGreaterThan(value, 0); }
    }

    public DateTime Date { get; }
}