using System.Collections.ObjectModel;
using RestaurantApp;
using RestaurantApp.Data.Repository;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;
using RestaurantAppUI.Presentation.Utils;
using RestaurantAppUI.Presentation.Formatter;
using MenuItem = RestaurantApp.Domain.Model.MenuItem;

namespace RestaurantAppUI.Presentation.Pages.RegistrationActions;

public partial class AddMenuItemPage : ContentPage
{
    private readonly IProductRepository _productRepository = ServiceLocator.GetService<IProductRepository>();
    private readonly IFormatter _formatter = ServiceLocator.GetService<IFormatter>();
    public List<MenuItemGroup> Groups { get; } = Enum.GetValues<MenuItemGroup>().ToList();
    public List<SavedModel<Product>> Products { get; }
    public ObservableCollection<Ingredient> Ingredients { get; } = [];

    public AddMenuItemPage()
    {
        Products = _productRepository.FindAll();
        InitializeComponent();
        BindingContext = this;
    }

    private void UpdateIngredients()
    {
        IngredientsLabel.Text = BuildIngredientsString();
    }

    private string BuildIngredientsString()
    {
        return string.Join("\n", Ingredients.Select((ingredient) => _formatter.Format(ingredient)));
    }

    private void OnAddIngredientClicked(object sender, EventArgs e)
    {
        var builder = new Ingredient.Builder();
        var form = new ValidatedForm(
            [
                ValidatedForm.Decimal(IngredientQuantityEntry, (value) => builder.SetQuantity(value)),
                ValidatedForm.Picker(IngredientPicker, (index) => builder.SetProductId(Products[index].Id)),
            ]
        );
        if (form.Validate())
        {
            Ingredients.Add(builder.Build());
            UpdateIngredients();
        }
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var builder = new MenuItem.Builder();
        var form = new ValidatedForm(new List<IValidatedFormEntry>
        {
            ValidatedForm.String(NameEntry, value => builder.SetName(value)),
            ValidatedForm.Picker(GroupPicker, index => builder.SetGroup(Groups[index])),
            ValidatedForm.Decimal(PriceEntry, value => builder.SetPrice(value)),
            ValidatedForm.Runnable(() => builder.SetIngredients(Ingredients),
                onError: _ => DisplayAlert("Ингредиенты",
                    "Добавьте хотя бы один ингредиент", "Ок")),
        });
        if (form.Validate())
        {
            var model = builder.Build();
            var menuItemRepository = ServiceLocator.GetService<IMenuItemRepository>();
            menuItemRepository.Add(model);
            await DisplayAlert("Успех", $"Информация о пункте меню {model.Name} была сохранена.", "Ок");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Ошибка валидации", "Все поля должны быть заполнены верно перед сохранение.", "Ок");
        }
    }
}