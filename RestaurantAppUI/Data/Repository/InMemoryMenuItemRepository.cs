using RestaurantAppUI.Domain.Model;
using MenuItem = RestaurantAppUI.Domain.Model.MenuItem;
using Model_MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Data.Repository;

public class InMemoryMenuItemRepository(List<SavedModel<MenuItem>> storage)
    : InMemoryBaseRepository<Model_MenuItem>(storage), IMenuItemRepository;