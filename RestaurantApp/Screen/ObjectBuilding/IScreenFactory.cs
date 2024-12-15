namespace RestaurantApp.Screen.ObjectBuilding;

public interface IScreenFactory
{
    public Screen CreateScreen();
}

public interface IParametrizedScreenFactory<in TParam>
{
    public Screen CreateScreen(TParam param);
}