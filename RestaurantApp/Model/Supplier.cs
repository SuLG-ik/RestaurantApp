using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public class Supplier
{
    public string Name { get; }
    public string Address { get; }
    public string Director { get; }
    public string PhoneNumber { get; }
    public string Bank { get; }
    public string AccountNumber { get; }
    public string Inn { get; }

    [JsonConstructor]
    private Supplier(string name, string address, string director, string phoneNumber, string bank,
        string accountNumber,
        string inn)
    {
        Name = name;
        Address = address;
        Director = director;
        PhoneNumber = phoneNumber;
        Bank = bank;
        AccountNumber = accountNumber;
        Inn = inn;
    }

    public class Builder
    {
        private string? _name;
        private string? _address;
        private string? _director;
        private string? _phone;
        private string? _bank;
        private string? _accountNumber;
        private string? _inn;

        public Builder SetName(string name)
        {
            _name = Validator.RequireNotBlank(name, nameof(name));
            return this;
        }

        public Builder SetAddress(string address)
        {
            _address = Validator.RequireNotBlank(address, nameof(address));
            return this;
        }

        public Builder SetDirector(string director)
        {
            _director = Validator.RequireNotBlank(director, nameof(director));
            return this;
        }

        public Builder SetPhoneNumber(string phone)
        {
            _phone = Validator.RequireNumeric(Validator.RequireNotBlank(phone, nameof(phone)), nameof(phone));
            return this;
        }

        public Builder SetBank(string bank)
        {
            _bank = Validator.RequireNotBlank(bank, nameof(bank));
            return this;
        }

        public Builder SetAccountNumber(string accountNumber)
        {
            _accountNumber = Validator.RequireNumeric(Validator.RequireNotBlank(accountNumber, nameof(accountNumber)),
                nameof(accountNumber));
            return this;
        }


        public Builder SetInn(string inn)
        {
            _inn = Validator.RequireNumeric(Validator.RequireNotBlank(inn, nameof(inn)), nameof(inn));
            return this;
        }

        public Supplier Build()
        {
            var name = Validator.RequireNotNull(_name, nameof(_name));
            var address = Validator.RequireNotNull(_address, nameof(_address));
            var director = Validator.RequireNotNull(_director, nameof(_director));
            var phone = Validator.RequireNotNull(_phone, nameof(_phone));
            var bank = Validator.RequireNotNull(_bank, nameof(_bank));
            var accountNumber = Validator.RequireNotNull(_accountNumber, nameof(_accountNumber));
            var inn = Validator.RequireNotNull(_inn, nameof(_inn));

            return new Supplier(name, address, director, phone, bank, accountNumber, inn);
        }
    }
}