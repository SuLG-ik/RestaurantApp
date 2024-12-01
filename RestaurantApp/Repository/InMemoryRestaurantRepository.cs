using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemoryRestaurantRepository: InMemoryBaseRepository<Restaurant>, IRestaurantRepository;