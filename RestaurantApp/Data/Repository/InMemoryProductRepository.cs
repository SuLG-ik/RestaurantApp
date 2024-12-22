using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantApp.Data.Repository;

public class InMemoryProductRepository(List<SavedModel<Product>> storage) :
    InMemoryBaseRepository<Product>(storage), IProductRepository;