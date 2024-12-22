using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantApp.Data.Repository;

public class InMemoryRestaurantRepository(List<SavedModel<Restaurant>> storage)
    : InMemoryBaseRepository<Restaurant>(storage), IRestaurantRepository;