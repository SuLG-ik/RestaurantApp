using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public class InMemoryProductRepository(List<SavedModel<Product>> storage) :
    InMemoryBaseRepository<Product>(storage), IProductRepository;