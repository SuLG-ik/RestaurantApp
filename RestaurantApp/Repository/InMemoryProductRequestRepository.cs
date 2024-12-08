using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemoryProductRequestRepository : InMemoryBaseRepository<ProductRequest>, IProductRequestRepository;