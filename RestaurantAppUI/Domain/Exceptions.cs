namespace RestaurantAppUI.Domain;

public class EntityNotFoundException(long id, string? tag = null) : Exception
{
    public long Id { get; } = id;
    override public string Message => $"Entity ({tag}) with id {Id} not found";
}
