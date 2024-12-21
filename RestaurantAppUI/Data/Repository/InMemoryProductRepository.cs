using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Data.Repository;

public class InMemoryProductRepository(List<SavedModel<Product>> storage) :
    InMemoryBaseRepository<Product>(storage), IProductRepository;