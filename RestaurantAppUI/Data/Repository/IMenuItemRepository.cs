using RestaurantAppUI.Domain.Repository;
using Model_MenuItem = RestaurantAppUI.Domain.Model.MenuItem;

namespace RestaurantAppUI.Data.Repository;

public interface IMenuItemRepository : IRepository<Model_MenuItem>;