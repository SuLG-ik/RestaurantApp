using RestaurantAppUI.Domain.Model;
using RestaurantAppUI.Domain.Repository;

namespace RestaurantAppUI.Data.Repository;

public class InMemorySupplierRepository(List<SavedModel<Supplier>> storage)
    : InMemoryBaseRepository<Supplier>(storage), ISupplierRepository;