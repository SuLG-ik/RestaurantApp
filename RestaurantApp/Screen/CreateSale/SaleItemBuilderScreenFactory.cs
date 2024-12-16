using RestaurantApp.Model;
using RestaurantApp.Screen.ObjectBuilding;

namespace RestaurantApp.Screen.CreateSale;

public class SaleItemBuilderScreenFactory(Func<int> restaurantId)
    : IParametrizedScreenFactory<MultipleItemsParams<SaleItem>>
{
    public Screen CreateScreen(MultipleItemsParams<SaleItem> param)
    {
        return new SaleItemBuilderScreen(restaurantId(), param.Action, param.Items);
    }
}