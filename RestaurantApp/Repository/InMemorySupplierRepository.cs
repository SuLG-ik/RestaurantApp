using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemorySupplierRepository(List<SavedModel<Supplier>> storage)
    : InMemoryBaseRepository<Supplier>(storage), ISupplierRepository;