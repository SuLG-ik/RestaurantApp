using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemorySupplierRepository : InMemoryBaseRepository<Supplier>, ISupplierRepository;