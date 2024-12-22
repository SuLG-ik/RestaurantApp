using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantApp.Data.Repository;

public class InMemorySupplierRepository(List<SavedModel<Supplier>> storage)
    : InMemoryBaseRepository<Supplier>(storage), ISupplierRepository;