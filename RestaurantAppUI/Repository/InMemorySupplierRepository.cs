using RestaurantAppUI.Model;

namespace RestaurantAppUI.Repository;

public class InMemorySupplierRepository(List<SavedModel<Supplier>> storage)
    : InMemoryBaseRepository<Supplier>(storage), ISupplierRepository;