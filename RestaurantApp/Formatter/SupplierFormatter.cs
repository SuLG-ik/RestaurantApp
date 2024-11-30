using System.Text;
using RestaurantApp.Model;

namespace RestaurantApp.Formatter;

public class SupplierFormatter : BaseFormatter<Supplier>
{
    protected override string Format(Supplier value)
    {
        return new StringBuilder().Append("Поставщик: ")
            .Append("Наименование: ").Append(value.Name)
            .Append(", адрес: ").Append(value.Address)
            .Append(", директор: ").Append(value.Director)
            .Append(", телефон: ").Append(value.PhoneNumber)
            .Append(", банк: ").Append(value.Bank)
            .Append(", номер счета: ").Append(value.AccountNumber)
            .Append(", ИНН: ").Append(value.Inn)
            .ToString();
    }
}
