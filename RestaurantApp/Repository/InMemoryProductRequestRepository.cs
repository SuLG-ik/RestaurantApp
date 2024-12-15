using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemoryProductRequestRepository(List<SavedModel<ProductRequest>> storage)
    : InMemoryBaseRepository<ProductRequest>(storage), IProductRequestRepository;