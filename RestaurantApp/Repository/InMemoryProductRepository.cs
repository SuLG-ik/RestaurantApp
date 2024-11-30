using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemoryProductRepository : InMemoryBaseRepository<Product>, IProductRepository;