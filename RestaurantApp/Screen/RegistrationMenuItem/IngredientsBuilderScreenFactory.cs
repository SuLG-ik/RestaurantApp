using RestaurantApp.Model;
using RestaurantApp.Screen.ObjectBuilding;
using RestaurantApp.Screen.RegistrationProduct;

namespace RestaurantApp.Screen.RegistrationMenuItem;

public class IngredientsBuilderScreenFactory : IParametrizedScreenFactory<MultipleItemsParams<Ingredient>>
{
    public Screen CreateScreen(MultipleItemsParams<Ingredient> param)
    {
        return new IngredientsBuilderScreen(param.Action);
    }
}