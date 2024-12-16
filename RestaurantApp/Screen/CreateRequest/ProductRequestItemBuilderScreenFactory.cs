using RestaurantApp.Model;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.CreateRequest;

public class
    ProductRequestItemBuilderScreenFactory : IParametrizedScreenFactory<MultipleItemsParams<ProductRequestItem>>
{
    public Screen CreateScreen(MultipleItemsParams<ProductRequestItem> param)
    {
        return new ProductRequestItemBuilderScreen(param.Action, param.Items);
    }
}