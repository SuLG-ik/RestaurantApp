namespace RestaurantApp.Data.Repository;

public class ConcurrentIdGenerator(int initialId = 0) : IIdGenerator
{
    private int _id = initialId;

    public int NextId()
    {
        Interlocked.Increment(ref _id);
        return _id;
    }
}