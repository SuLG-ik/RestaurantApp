using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

[method: JsonConstructor]
public class ProductDeduction(int restaurantId, DateTime date, int productId, decimal quantity)
{
    public int RestaurantId { get; } = Validator.RequireGreaterThan(restaurantId, 0, nameof(restaurantId));
    public DateTime Date { get; } = date;
    public int ProductId { get; } = productId;
    public decimal Quantity { get; } = Validator.RequireGreaterThan(quantity, 0, nameof(quantity));
}