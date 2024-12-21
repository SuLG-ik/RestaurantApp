using RestaurantAppUI.Model;

namespace RestaurantAppUI.Pages.RegistrationActions;

public partial class AddSupplierPage : ContentPage
{
    public AddSupplierPage()
    {
        InitializeComponent();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var builder = new Supplier.Builder();
        var form = new ValidatedForm([
            ValidatedForm.String(NameEntry, (value) => builder.SetName(value)),
            ValidatedForm.String(AddressEntry, (value) => builder.SetAddress(value)),
            ValidatedForm.String(DirectorEntry, (value) => builder.SetDirector(value)),
            ValidatedForm.String(PhoneNumberEntry, (value) => builder.SetPhoneNumber(value)),
            ValidatedForm.String(BankEntry, (value) => builder.SetBank(value)),
            ValidatedForm.String(AccountNumberEntry, (value) => builder.SetAccountNumber(value)),
            ValidatedForm.String(IinEntry, (value) => builder.SetInn(value)),
        ]);
        if (form.Validate())
        {
            var model = builder.Build();
            await DisplayAlert("Успех", $"Информация о поставщике {model.Name} была сохранена.", "Ок");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Ошибка валидации", "Все поля должны быть заполнены верно перед сохранение.", "Ок");
        }
    }
}