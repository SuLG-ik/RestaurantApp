using RestaurantAppUI.Presentation.Utils;
using System.Collections.ObjectModel;
using RestaurantApp;
using RestaurantApp.Domain.Model;
using RestaurantApp.Domain.Repository;

namespace RestaurantAppUI.Presentation.Pages.RegistrationActions;

public partial class AddProductPage : ContentPage
{
    private readonly ISupplierRepository _supplierRepository = ServiceLocator.GetService<ISupplierRepository>();
    public List<Unit> Units => Enum.GetValues<Unit>().ToList();
    public List<SavedModel<Supplier>> Suppliers => _supplierRepository.FindAll();

    public AddProductPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var builder = new Product.Builder();
        var form = new ValidatedForm(new List<IValidatedFormEntry>
        {
            ValidatedForm.String(NameEntry, value => builder.SetName(value)),
            ValidatedForm.Picker(UnitPicker, index => builder.SetUnit(Units[index])),
            ValidatedForm.Decimal(QuantityEntry, value => builder.SetQuantity(value)),
            ValidatedForm.Decimal(PriceEntry, value => builder.SetPrice(value)),
            ValidatedForm.Picker(SupplierPicker,
                index => builder.SetSupplierId(Suppliers[index].Id)),
        });
        if (form.Validate())
        {
            var model = builder.Build();
            var productRepository = ServiceLocator.GetService<IProductRepository>();
            productRepository.Add(model);
            await DisplayAlert("Успех", $"Информация о продукте {model.Name} была сохранена.", "Ок");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Ошибка валидации", "Все поля должны быть заполнены верно перед сохранение.", "Ок");
        }
    }
}