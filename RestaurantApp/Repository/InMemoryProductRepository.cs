using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemoryProductRepository(List<SavedModel<Product>> storage) :
    InMemoryBaseRepository<Product>(storage), IProductRepository;