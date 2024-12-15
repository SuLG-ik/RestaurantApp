using RestaurantApp.Model;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.RegistrationRestaurant;

public class MenuItemBuilderScreenFactory : IParametrizedScreenFactory<Action<MenuItem>>
{
    public Screen CreateScreen(Action<MenuItem> param)
    {
        return new MenuItemBuilderScreen(param);
    }
}