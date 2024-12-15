using RestaurantApp.Model;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationRestaurant;

public class MenuItemBuilderScreenFactory : IParametrizedScreenFactory<MultipleItemsParams<MenuItem>>
{
    public Screen CreateScreen(MultipleItemsParams<MenuItem> param)
    {
        return new MenuItemBuilderScreen(param.Action);
    }
}