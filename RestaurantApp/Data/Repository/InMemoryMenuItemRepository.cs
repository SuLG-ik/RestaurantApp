using RestaurantApp.Domain.Model;
using Domain_Model_MenuItem = RestaurantApp.Domain.Model.MenuItem;
using MenuItem = RestaurantApp.Domain.Model.MenuItem;

namespace RestaurantApp.Data.Repository;

public class InMemoryMenuItemRepository(List<SavedModel<MenuItem>> storage)
    : InMemoryBaseRepository<Domain_Model_MenuItem>(storage), IMenuItemRepository;