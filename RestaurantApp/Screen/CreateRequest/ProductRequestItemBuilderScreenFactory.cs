using RestaurantApp.Model;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.CreateRequest;

public class ProductRequestItemBuilderScreenFactory : IParametrizedScreenFactory<Action<ProductRequestItem>>
{
    public Screen CreateScreen(Action<ProductRequestItem> param)
    {
        return new ProductRequestItemBuilderScreen(param);
    }
}