using RestaurantApp.Model;

namespace RestaurantApp.Repository;

public class InMemoryMenuItemRepository(List<SavedModel<MenuItem>> storage)
    : InMemoryBaseRepository<MenuItem>(storage), IMenuItemRepository;