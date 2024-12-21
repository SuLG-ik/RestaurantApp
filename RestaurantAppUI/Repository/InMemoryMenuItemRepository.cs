using RestaurantAppUI.Model;
using MenuItem = RestaurantAppUI.Model.MenuItem;

namespace RestaurantAppUI.Repository;

public class InMemoryMenuItemRepository(List<SavedModel<MenuItem>> storage)
    : InMemoryBaseRepository<MenuItem>(storage), IMenuItemRepository;